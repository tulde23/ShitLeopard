namespace ShitLeopard.Api
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Threading.Tasks.Dataflow;

    public class BufferredQueue<TData> : IDisposable
    {
        private readonly BatchBlock<TData> _batchBlock;
        private readonly int _interval;
        private readonly Timer _timer;
        private readonly TransformBlock<TData, TData> _timingBlock;
        private IBufferedTarget<TData> _bufferedTarget;
        private Func<IEnumerable<TData>, Task> _continuation;

        private BufferredQueue(BufferredQueueOptions options)
        {
            _interval = options.BufferSize * 1000;
            _batchBlock = new BatchBlock<TData>(options.BufferSize, options.GroupingDataflowBlockOptions ?? new GroupingDataflowBlockOptions { Greedy = true });
            _timer = new Timer(_ =>
            {
                _batchBlock.TriggerBatch();
            }, null, _interval, _interval);

            Func<TData, TData> resetTimerIdentity = value =>

            {
                _timer.Change(_interval, Timeout.Infinite);
                return value;
            };

            var messageAction = new ActionBlock<IEnumerable<TData>>(async (messages) => await TryReceiveThrottledMessage(messages));
            _timingBlock = new TransformBlock<TData, TData>(resetTimerIdentity);

            //the order is important.
            _timingBlock.LinkTo(_batchBlock);
            _batchBlock.LinkTo(messageAction);
        }

        public BufferredQueue(BufferredQueueOptions options, Func<IEnumerable<TData>, Task> continuation) : this(options)
        {
            _continuation = continuation;
        }

        public BufferredQueue(BufferredQueueOptions options, IBufferedTarget<TData> bufferedTarget) : this(options)
        {
            _bufferedTarget = bufferedTarget;
        }

        public async Task SendAsync(TData data)
        {
            await _timingBlock.SendAsync(data);
        }

        protected Task TryReceiveThrottledMessage(IEnumerable<TData> messages)
        {
            if (!_disposedValue)
            {
                if (_continuation != null)
                {
                    return _continuation?.Invoke(messages);
                }
                else if (_bufferedTarget != null)
                {
                    _bufferedTarget.ReceiveAsync(messages);
                }
            }
            return Task.CompletedTask;
        }

        #region IDisposable Support

        private bool _disposedValue = false; // To detect redundant calls

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _continuation = null;
                    _bufferedTarget = null;
                    _batchBlock.Complete();
                    _timingBlock.Complete();
                    _timer.Change(Timeout.Infinite, Timeout.Infinite);
                    _timer.Dispose();
                }

                _disposedValue = true;
            }
        }

        #endregion IDisposable Support
    }

    public interface IBufferedTarget<in TData>
    {
        public Task ReceiveAsync(IEnumerable<TData> data);
    }

    /// <summary>
    /// Implements both a time based and buffer based threading block.
    /// </summary>
    public class BufferredQueueOptions
    {
        /// <summary>
        /// Gets or sets the size of the buffer.  This is how many items will be kept in memory.  When the threshold is reached, the items will be release and the
        /// call back will be invoked.
        /// </summary>
        /// <value>
        /// The size of the buffer.
        /// </value>
        public int BufferSize { get; set; }

        /// <summary>
        /// Gets or sets the batch timeout in seconds.  the number of seconds elapsed since the last item was sent.  If this threshold is met, the items will be released and the timer reset.
        /// </summary>
        /// <value>
        /// The batch timeout in seconds.
        /// </value>
        public int Expiration { get; set; }

        public GroupingDataflowBlockOptions GroupingDataflowBlockOptions { get; set; } = new GroupingDataflowBlockOptions { Greedy = true };
    }
}