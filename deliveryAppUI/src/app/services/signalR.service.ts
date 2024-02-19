import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  private hubConnection?: signalR.HubConnection;

  private messageSubject = new Subject<string>();
  public message$ = this.messageSubject.asObservable();

  constructor() {
    this.startConnection();
    this.registerOnServerEvents();
  }

  private startConnection() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5000/websocket') // Substitua pela URL do seu servidor SignalR
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Conectado ao servidor SignalR'))
      .catch((err) => console.error(`Erro ao conectar: ${err}`));
  }

  private registerOnServerEvents(): void {
    this.hubConnection!.on('ReceiveMessage', (data: string) => {
      this.messageSubject.next(data);
    });
  }
}
