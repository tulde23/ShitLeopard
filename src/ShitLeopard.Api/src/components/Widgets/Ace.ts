export default {
    name: 'AceEditor',
    props: ['editorId', 'content', 'lang', 'theme', 'width', 'height'],
    data: function() {
      return {
        editor: Object,
        beforeContent: '',
        editorName: `ace${this.editorId}`,
        editorWidth: this.width || 500,
        editorHeight: this.height || 400
      };
    },
    methods: {},
    mounted: function() {
      const lang = this.lang || 'json';
      const theme = this.theme || 'tomorrow_night_eighties';
      let text = '';
      this.editor = window["ace"].edit(this.editorName);
      if (this.content) {
       if (typeof this.content === 'string' || this.content instanceof String) {
           text = this.content;
         
        } else {
          text = JSON.stringify(this.content, null, 2);
        }
  
       this.editor.setValue(text, 1);
      }
  
      this.editor.getSession().setMode(`ace/mode/${lang}`);
      this.editor.setTheme(`ace/theme/${theme}`);
  
      this.editor.on('change', () => {
        this.beforeContent = this.editor.getValue();
        this.$emit('change-content', this.editor.getValue());
      });
    },
    watch: {
      content(value) {
        let text = '';
       // console.log("value",value);
        if (typeof value === 'string') {
          const model = JSON.parse( value);
         // console.log("value", value);
          text = JSON.stringify(model, null, 2);
          console.log('model', model);
        } else {
          text = JSON.stringify(this.content, null, 2);
        }
        if (this.beforeContent !== text) {
            this.editor.setValue(text, 1);
          }
      }
    }
  };