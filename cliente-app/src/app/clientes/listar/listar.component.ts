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
  clientesFiltrados: any[] = [];
  paginaAtual = 1;
  itensPorPagina = 10;
  apiUrl = 'https://localhost:5001/api/Cliente';

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit() {
    this.listarClientes();
  }

  listarClientes() {
    this.http.get<any[]>(this.apiUrl).subscribe({
      next: (response) => {
        this.clientes = response;
        this.clientesFiltrados = [...this.clientes];
      },
      error: (error) => {
        console.error('Erro ao listar clientes:', error);
      }
    });
  }

  clientesPaginados() {
    const inicio = (this.paginaAtual - 1) * this.itensPorPagina;
    const fim = inicio + this.itensPorPagina;
    return this.clientesFiltrados.slice(inicio, fim);
  }

  paginaAnterior() {
    if (this.paginaAtual > 1) {
      this.paginaAtual--;
    }
  }

  proximaPagina() {
    if (this.paginaAtual < this.totalPaginas()) {
      this.paginaAtual++;
    }
  }

  totalPaginas() {
    return Math.ceil(this.clientesFiltrados.length / this.itensPorPagina);
  }

  navegarParaCadastro() {
    this.router.navigate(['/clientes/cadastrar']);
  }

editarCliente(cliente: any) {
  this.router.navigate(['/clientes/cadastrar'], { queryParams: { id: cliente.id } });
}



  excluirCliente(cliente: any) {
    if (confirm(`Deseja realmente excluir o cliente ${cliente.nomeRazaoSocial}?`)) {
      this.http.delete(`${this.apiUrl}/${cliente.id}`).subscribe({
        next: () => {
          alert('Cliente excluído com sucesso.');
          this.listarClientes();
        },
        error: (error) => {
          console.error('Erro ao excluir cliente:', error);
        }
      });
    }
  }
}
