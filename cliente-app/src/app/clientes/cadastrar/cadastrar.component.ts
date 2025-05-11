import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Router, ActivatedRoute, RouterModule,Params } from '@angular/router';


@Component({
  selector: 'app-cadastrar',
  templateUrl: './cadastrar.component.html',
  styleUrls: ['./cadastrar.component.scss'],
  standalone: true,
  imports: [ReactiveFormsModule, FormsModule, CommonModule, HttpClientModule, RouterModule]
})
export class CadastrarComponent implements OnInit {
  clienteForm!: FormGroup;
  clienteEditando: any = null;
  apiUrl = 'https://localhost:5001/api/Cliente'; // Verifique a URL da sua API

  constructor(
    private fb: FormBuilder,
    private httpClient: HttpClient,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.inicializarFormulario();
    this.carregarClienteParaEdicao();
  }

  inicializarFormulario() {
    this.clienteForm = this.fb.group({
      NomeRazaoSocial: ['', Validators.required],
      CpfCnpj: ['', Validators.required],
      Tipo: ['1', Validators.required],
      DataNascimento: [''],
      InscricaoEstadual: [''],
      IsentoIE: [false],
      Telefone: [''],
      Email: ['', [Validators.required, Validators.email]],
      Cep: [''],
      Endereco: [''],
      Numero: [''],
      Complemento: [''],
      Bairro: [''],
      Cidade: [''],
      Estado: ['']
    });

    this.atualizarTipoPessoa();
  }

carregarClienteParaEdicao() {
  const clienteId = this.route.snapshot.queryParamMap.get('id');
  console.log('ID do Cliente via snapshot:', clienteId); 

  if (clienteId) {
    const apiUrlCompleta = `https://localhost:5001/api/Cliente/${clienteId}`;
    console.log('URL da API:', apiUrlCompleta); // ✅ Verificação para debug

    

    this.httpClient.get(apiUrlCompleta).subscribe({
      next: (cliente: any) => {
        console.log('Cliente carregado para edição:', cliente);
        this.clienteEditando = cliente;
        this.clienteForm.patchValue(cliente);   
        this.clienteEditando = { ...cliente }; // Mantendo o ID no objeto

      const tipoPessoa =
        cliente.tipo === "Fisica" ? "1" :
        cliente.tipo === "Juridica" ? "2" :
        "1"; // Padrão para Pessoa Física

      this.clienteForm.patchValue({
        NomeRazaoSocial: cliente.nomeRazaoSocial || '',
        CpfCnpj: cliente.cpfCnpj || '',
        Tipo: tipoPessoa, // ✅ Aplicando o valor correto
        DataNascimento: cliente.dataNascimento ? cliente.dataNascimento : '',
        InscricaoEstadual: cliente.inscricaoEstadual || '',
        IsentoIE: cliente.isentoIE || false,
        Telefone: cliente.telefone || '',
        Email: cliente.email || '',
        Cep: cliente.cep || '',
        Endereco: cliente.endereco || '',
        Numero: cliente.numero || '',
        Complemento: cliente.complemento || '',
        Bairro: cliente.bairro || '',
        Cidade: cliente.cidade || '',
        Estado: cliente.estado || ''
      });
        this.atualizarTipoPessoa();
      },
      error: (error) => {
        console.error('Erro ao carregar cliente:', error);
        if (error.status === 404) {
          alert('Cliente não encontrado.');
        } else if (error.status === 401 || error.status === 403) {
          alert('Acesso negado. Verifique suas credenciais.');
        } else {
          alert('Erro ao carregar o cliente para edição.');
        }
      }
    });
  } else {
    console.warn('ID do cliente não encontrado nos parâmetros.');
  }
}

  atualizarTipoPessoa() {
    this.clienteForm.get('Tipo')?.valueChanges.subscribe(tipo => {
      if (tipo == '1') {
        // Pessoa Física
        this.clienteForm.get('DataNascimento')?.setValidators([Validators.required]);
        this.clienteForm.get('InscricaoEstadual')?.clearValidators();
        this.clienteForm.get('InscricaoEstadual')?.setValue('');
        this.clienteForm.get('IsentoIE')?.setValue(false);
        this.clienteForm.get('InscricaoEstadual')?.disable();
      } else {
        // Pessoa Jurídica
        this.clienteForm.get('DataNascimento')?.clearValidators();
        this.clienteForm.get('DataNascimento')?.setValue('');
        this.clienteForm.get('InscricaoEstadual')?.enable();
        this.aplicarRegraInscricaoEstadual();
      }

      this.clienteForm.get('DataNascimento')?.updateValueAndValidity();
      this.clienteForm.get('InscricaoEstadual')?.updateValueAndValidity();
    });
  }

  aplicarRegraInscricaoEstadual() {
    const isento = this.clienteForm.get('IsentoIE')?.value;
    const inscricaoEstadualControl = this.clienteForm.get('InscricaoEstadual');

    if (isento) {
      inscricaoEstadualControl?.setValue('');
      inscricaoEstadualControl?.disable();
    } else {
      inscricaoEstadualControl?.enable();
    }

    inscricaoEstadualControl?.updateValueAndValidity();
  }

  cadastrarCliente() {
    const clienteData = { ...this.clienteForm.value };

    clienteData.IsentoIE = clienteData.IsentoIE ?? false;
    clienteData.DataNascimento = clienteData.DataNascimento === "" ? null : clienteData.DataNascimento;
    clienteData.InscricaoEstadual = clienteData.IsentoIE ? null : clienteData.InscricaoEstadual || null;

    if (this.clienteForm.valid) {
      if (this.clienteEditando) {
         clienteData.id = this.clienteEditando.id;
        this.httpClient.put(`${this.apiUrl}/${this.clienteEditando.id}`, clienteData).subscribe({
          next: () => {
            alert('Cliente atualizado com sucesso!');
            this.router.navigate(['/clientes/listar']);
          },
          error: (error) => {
            console.error('Erro ao atualizar cliente:', error);
          }
        });
      } else {
        this.httpClient.post(this.apiUrl, clienteData).subscribe({
          next: () => {
            alert('Cliente cadastrado com sucesso!');
            this.router.navigate(['/clientes/listar']);
          },
          error: (error) => {
            console.error('Erro ao cadastrar cliente:', error);
            this.exibirErrosAPI(error);
          }
        });
      }
    } else {
      this.exibirErrosFormulario();
    }
  }

  limparFormulario() {
    this.clienteForm.reset();
    this.clienteForm.patchValue({ Tipo: '1' });
    this.clienteEditando = null;
  }

  voltartelalistagem() {
    this.router.navigate(['/clientes/listar']);
  }

  exibirErrosFormulario() {
    const erros = Object.keys(this.clienteForm.controls)
      .filter(campo => this.clienteForm.get(campo)?.invalid)
      .map(campo => this.obterNomeCampo(campo));
    
    alert(`Formulário Inválido! Verifique os campos:\n${erros.join('\n')}`);
  }

  exibirErrosAPI(error: any) {
    console.error('Erro ao cadastrar/atualizar cliente:', error);
    if (error.error && error.error.errors) {
      const mensagens = Object.keys(error.error.errors)
        .map(campo => `- ${campo}: ${error.error.errors[campo].join(', ')}`)
        .join('\n');
      alert(`Erro de Validação:\n\n${mensagens}`);
    } else {
      alert("Erro desconhecido ao cadastrar/atualizar cliente.");
    }
  }

  obterNomeCampo(campo: string): string {
    const nomes: Record<string, string> = {
      NomeRazaoSocial: 'Nome/Razão Social',
      CpfCnpj: 'CPF/CNPJ',
      Tipo: 'Tipo de Pessoa',
      DataNascimento: 'Data de Nascimento',
      InscricaoEstadual: 'Inscrição Estadual',
      Email: 'Email',
      Telefone: 'Telefone',
      Cep: 'CEP',
      Endereco: 'Endereço',
      Numero: 'Número',
      Complemento: 'Complemento',
      Bairro: 'Bairro',
      Cidade: 'Cidade',
      Estado: 'Estado'
    };

    return nomes[campo as keyof typeof nomes] || campo;
  }
}
