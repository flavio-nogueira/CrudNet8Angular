import { Component } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-clientes',
  standalone: true,
  imports: [RouterModule, RouterOutlet],
  template: `
    <h2>Clientes</h2>
    <nav>
      <a routerLink="listar">Listar</a> |
      <a routerLink="cadastrar">Cadastrar</a>
    </nav>
    <router-outlet></router-outlet>
  `,
})
export class ClientesComponent {}
