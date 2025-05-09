import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-cadastrar',
  templateUrl: './cadastrar.component.html',
  styleUrls: ['./cadastrar.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule, HttpClientModule, RouterModule]
})
export class CadastrarComponent implements OnInit {
  clienteForm!: FormGroup;

  constructor(private fb: FormBuilder, private httpClient: HttpClient) {}

  ngOnInit() {
    this.inicializarFormulario();
  }

inicializarFormulario() {
  this.clienteForm = this.fb.group({
    NomeRazaoSocial: [''],
    CpfCnpj: [''],
    Tipo: [''],
    DataNascimento: [''], // Apenas para pessoa física
    InscricaoEstadual: [''], // Apenas para pessoa jurídica
    IsentoIE: [false], // Apenas para pessoa jurídica
    Telefone: [''],
    Email: [''],
    Cep: [''],
    Endereco: [''],
    Numero: [''],
    Complemento: [''],
    Bairro: [''],
    Cidade: [''],
    Estado: ['']
  });
  
}

atualizarTipoPessoa() {
  const tipo = this.clienteForm.get('Tipo')?.value;

  if (tipo == '1') {
    // Pessoa Física
    this.clienteForm.get('DataNascimento')?.setValidators([Validators.required]);
    this.clienteForm.get('InscricaoEstadual')?.clearValidators();
    this.clienteForm.get('InscricaoEstadual')?.setValue('');
    this.clienteForm.get('IsentoIE')?.setValue(false);
  } else if (tipo == '2') {
    // Pessoa Jurídica
    this.clienteForm.get('DataNascimento')?.clearValidators();
    this.clienteForm.get('DataNascimento')?.setValue('');
    this.clienteForm.get('InscricaoEstadual')?.setValidators([Validators.required]);
  }

  this.clienteForm.get('DataNascimento')?.updateValueAndValidity();
  this.clienteForm.get('InscricaoEstadual')?.updateValueAndValidity();
}
atualizarCampos() {
  const tipo = this.clienteForm.get('Tipo')?.value;
  if (tipo == 1) {
    this.clienteForm.get('DataNascimento')?.setValidators(Validators.required);
    this.clienteForm.get('InscricaoEstadual')?.clearValidators();
  } else {
    this.clienteForm.get('DataNascimento')?.clearValidators();
    this.clienteForm.get('InscricaoEstadual')?.setValidators(Validators.required);
  }
  this.clienteForm.updateValueAndValidity();
}


cadastrarCliente() {
  if (this.clienteForm.valid) {
    // Criar o JSON manualmente para garantir que todos os campos estejam presentes
     const clienteData = {
      NomeRazaoSocial: this.clienteForm.get('NomeRazaoSocial')?.value,
      CpfCnpj: this.clienteForm.get('CpfCnpj')?.value,
      Tipo: this.clienteForm.get('Tipo')?.value,
      DataNascimento: this.clienteForm.get('DataNascimento')?.value,
      InscricaoEstadual: this.clienteForm.get('InscricaoEstadual')?.value,
      IsentoIE: this.clienteForm.get('IsentoIE')?.value,
      Telefone: this.clienteForm.get('Telefone')?.value,
      Email: this.clienteForm.get('Email')?.value,
      Cep: this.clienteForm.get('Cep')?.value,
      Endereco: this.clienteForm.get('Endereco')?.value,
      Numero: this.clienteForm.get('Numero')?.value,
      Complemento: this.clienteForm.get('Complemento')?.value,
      Bairro: this.clienteForm.get('Bairro')?.value,
      Cidade: this.clienteForm.get('Cidade')?.value,
      Estado: this.clienteForm.get('Estado')?.value
    }

    console.log('JSON Enviado para a API:', JSON.stringify(clienteData, null, 2));

    this.httpClient.post('https://localhost:5001/api/Cliente', clienteData).subscribe({
      next: (response) => {
        console.log('Cliente cadastrado com sucesso:', response);
        alert('Cliente cadastrado com sucesso!');
        this.limparFormulario();
      },
      error: (error) => {

        console.error('Erro ao cadastrar cliente:', error);
        this.exibirErrosAPI(error);
      }
    });
  } else {
    console.error('Formulário Inválido:', this.clienteForm);
    this.exibirErrosFormulario();
  }
}


  limparFormulario() {
    this.clienteForm.reset();
    Object.keys(this.clienteForm.controls).forEach((key) => {
      this.clienteForm.get(key)?.setErrors(null);
    });
  }

 exibirErrosFormulario() {
  const erros = Object.keys(this.clienteForm.controls)
    .filter(campo => this.clienteForm.get(campo)?.invalid)
    .map(campo => {
      const nomeCampo = this.obterNomeCampo(campo);
      return `- ${nomeCampo}`;
    });

  alert(`Formulário Inválido! Verifique os campos:\n${erros.join('\n')}`);
}

obterNomeCampo(campo: string): string {
  switch (campo) {
    case 'NomeRazaoSocial': return 'Nome/Razão Social';
    case 'CpfCnpj': return 'CPF/CNPJ';
    case 'Tipo': return 'Tipo de Pessoa';
    case 'DataNascimento': return 'Data de Nascimento';
    case 'Email': return 'Email';
    case 'Telefone': return 'Telefone';
    case 'Cep': return 'CEP';
    case 'Endereco': return 'Endereço';
    case 'Numero': return 'Número';
    case 'Complemento': return 'Complemento';
    case 'Bairro': return 'Bairro';
    case 'Cidade': return 'Cidade';
    case 'Estado': return 'Estado';
    default: return campo;
  }
}


  exibirErrosAPI(error: any) {
  console.error('Erro ao cadastrar cliente:', error); // Log para debug

  // Verifica se o erro contém o campo "errors" no formato esperado
  if (error.error && error.error.errors) {
    const titulo = error.error.title || "Erro de Validação";
    let mensagens = '';

    // Verificando se é um array ou um objeto
    if (Array.isArray(error.error.errors)) {
      mensagens = error.error.errors
        .map((err: any) => `- ${err.propertyName}: ${err.errorMessage}`)
        .join('\n');
    } else {
      // Tratando quando é um objeto com chaves e arrays de mensagens
      mensagens = Object.keys(error.error.errors)
        .map((campo) => `- ${campo}: ${error.error.errors[campo].join(', ')}`)
        .join('\n');
    }

    alert(`${titulo}\n\n${mensagens}`);
  } else {
    // Caso o formato do erro não seja o esperado
    alert("Erro desconhecido ao cadastrar cliente.");
  }
}

}
