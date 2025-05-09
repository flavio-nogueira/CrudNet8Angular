// src/app/clientes/cliente.service.ts
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Cliente {
  nomeRazaoSocial: string;
  cpfCnpj: string;
  tipo: string;
  dataNascimento?: string;
  inscricaoEstadual?: string;
  isentoIE?: boolean;
  telefone: string;
  email: string;
  cep: string;
  endereco: string;
  numero: string;
  bairro: string;
  cidade: string;
  estado: string;
}

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  private apiUrl = 'https://localhost:5001/api/Cliente';

  constructor(private http: HttpClient) {}

  cadastrarCliente(cliente: Cliente): Observable<any> {
    return this.http.post(this.apiUrl, cliente);
  }
}
