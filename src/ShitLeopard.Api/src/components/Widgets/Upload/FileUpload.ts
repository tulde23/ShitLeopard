import { Component, Watch, Vue, Prop } from 'vue-property-decorator';

const STATUS_INITIAL = 0;
const STATUS_SAVING = 1;
const STATUS_SUCCESS = 2;
const STATUS_FAILED = 3;


@Component({})
export default class FileUpload extends Vue {
    public uploadedFiles=[];
    public uploadError: null;
    public currentStatus: number;
    public uploadFieldName: 'users';

    public get isInitial() {
        return this.currentStatus === STATUS_INITIAL;
    }
    public get isSaving() {
        return this.currentStatus === STATUS_SAVING;
    }
    public get isSuccess() {
        return this.currentStatus === STATUS_SUCCESS;
    }
    public get isFailed() {
        return this.currentStatus === STATUS_FAILED;
    }
    public reset() {
        // reset form to initial state
        this.currentStatus = STATUS_INITIAL;
        this.uploadedFiles = [];
        this.uploadError = null;
    }
    public onFileChange(e: any) {
        const formData = new FormData();
        const files = e.target.files;
        console.log("files", files);
        if( files.length == 1){
            formData.append('onboard', files[0], files[0].name);
            this.save(formData);
        }
        
    }
    public save(formData) {
        // upload data to the server
        this.currentStatus = STATUS_SAVING;

        this.$upload.upload(formData)
            .then(x => {
                console.log("uploaded",x.data);
                this.uploadedFiles = x.data;
            })
            .catch(err => {
                this.uploadError = err.response;
          
            });
    }

    mounted() {
        this.reset();
    }
}


