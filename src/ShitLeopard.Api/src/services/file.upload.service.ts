
import axios from 'axios';
import { HttpService } from '@/services/http.service';
export class FileUploadService {

    constructor( public http: HttpService){

    }
    public upload(formData) {
        return this.http.post(`/api/v1/Users/Onboard`, formData)
            // get data
            .then(x => x.data);
        // add url field

    }
}