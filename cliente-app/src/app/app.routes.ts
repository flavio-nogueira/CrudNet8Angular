// app.routes.ts
import { Routes } from '@angular/router';
import { ListarComponent } from './clientes/listar/listar.component';
import { CadastrarComponent } from './clientes/cadastrar/cadastrar.component';

export const routes: Routes = [
  { path: '', redirectTo: 'clientes/listar', pathMatch: 'full' },
  { path: 'clientes/listar', component: ListarComponent },
  { path: 'clientes/cadastrar', component: CadastrarComponent },
  { path: '**', redirectTo: 'clientes/listar' }
];
