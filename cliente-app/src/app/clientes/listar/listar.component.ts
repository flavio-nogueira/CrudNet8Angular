import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-listar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './listar.component.html',
  styleUrls: ['./listar.component.scss']
})
export class ListarComponent {
  clientes: any[] = [];
  apiUrl = 'https://localhost:5001/api/Cliente'; // Verifique a URL da sua API

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.listarClientes();
  }

 listarClientes() {
    console.log('Chamando API:', this.apiUrl);
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (response) => {
        console.log('Clientes carregados:', response);
        this.clientes = response;
      },
      error: (error) => {
        console.error('Erro ao listar clientes:', error);
      }
    });
  }

    navegarParaCadastro() {
    this.router.navigate(['/clientes/cadastrar']);
  }

  editarCliente(cliente: any) {
    console.log('Editar:', cliente);
    // Implementar lógica de edição
  }

  excluirCliente(cliente: any) {
    console.log('Excluir:', cliente);
    // Implementar lógica de exclusão
  }
}
