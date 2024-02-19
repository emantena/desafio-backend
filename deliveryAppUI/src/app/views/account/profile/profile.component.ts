import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { UploadService } from 'src/app/services/upload.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})
export class ProfileComponent implements OnInit {
  selectedFile?: File;
  localUrl: any;
  file?: File;

  constructor(private uploadService: UploadService) {}

  ngOnInit(): void {}

  onFileChanged(event: Event) {
    const inputElement = event.target as HTMLInputElement;
    if (inputElement.files && inputElement.files.length > 0) {
      const file = inputElement.files[0];

      // this.onUpload(file);
    }
  }
  selectFile(event: any) {
    this.file = <File>event.target.files[0];
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.onload = (event: any) => {
        this.localUrl = event.target.result;
      };
      reader.readAsDataURL(event.target.files[0]);
      this.uploadFile();
    }
  }

  uploadFile() {
    if (this.file != undefined) {
      this.uploadService.uploadFile(this.file).subscribe((data) => {
        console.log(data);
        alert('Arquivo enviado com sucesso!');
      });
    } else {
      alert('Selecione um arquivo!');
    }
  }

  // onUpload(file: File) {
  //   const formData = new FormData();

  //   formData.append('file', file, file.name);

  //   formData.forEach((value, key) => {
  //     console.log('Key:', key, 'Value:', value);
  //   });

  //   const headers = new HttpHeaders({
  //     Accept: '*/*',
  //     'Content-Type':
  //       'multipart/form-data; boundary=<calculated when request is sent>',
  //   });

  //   this.httpClient
  //     .post(
  //       'https://localhost:44365/api/DeliveryMan/5/upload/document/cnh',
  //       formData,
  //       { headers }
  //     )
  //     .subscribe(
  //       (response) => {
  //         console.log('Upload bem-sucedido:', response);
  //       },
  //       (error) => {
  //         console.error('Erro no upload:', error);
  //       }
  //     );
  // }
}
