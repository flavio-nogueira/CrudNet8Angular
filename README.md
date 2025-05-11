
# CrudNet8Angular - Aplicação CRUD de Clientes

## 🚀 Visão Geral
Esta é uma aplicação completa de Cadastro de Clientes, composta por uma API desenvolvida em .NET 8.0 e um Front-End desenvolvido com Angular.

---

## ✅ Tecnologias Utilizadas e Justificativas
### Back-End
- **.NET 8.0 (Web API)**: Escolhido por ser uma versão moderna e estável, com suporte de longo prazo da Microsoft. Permite o uso de recursos atualizados de segurança e performance.
- **MySQL**: Um banco de dados relacional amplamente utilizado, escolhido pela sua simplicidade, desempenho e fácil integração com Docker.
- **CQRS e Event Sourcing**: Implementados para separar comandos (escrita) e queries (leitura), garantindo melhor organização, flexibilidade e performance da aplicação.
- **FluentValidation**: Ferramenta robusta para validação de entrada de dados, garantindo consistência e simplicidade no código de validação.
- **xUnit e Moq**: Frameworks de testes unitários amplamente utilizados, garantindo que a aplicação seja testada com segurança e facilidade.
- **AutoMapper**: Facilita o mapeamento entre objetos, evitando código repetitivo e garantindo uma transformação clara de dados.
- **Docker e Docker Compose**: Utilizados para garantir uma configuração consistente e fácil de replicar, simplificando o deploy e o gerenciamento do ambiente.

### SeedClientes (Carga Inicial de Dados)
- O método SeedClientes permite popular automaticamente o banco de dados com 40 registros de clientes, respeitando as regras de negócio:
  - Apenas um cadastro por CPF/CNPJ e E-mail.
  - Pessoas Físicas com idade mínima de 18 anos.
  - Pessoas Jurídicas com Inscrição Estadual ou Isenção de IE.
  - Esse método é útil para testes e desenvolvimento, garantindo um conjunto inicial de dados.

### Como Executar o SeedClientes com Docker Compose
1. Certifique-se de que o Docker e o Docker Compose estão instalados.
2. Navegue até a pasta do projeto:
```bash
cd D:\Desenvolvimento\CrudNet8Angular\BlackEnd
```
3. Execute o comando para subir o ambiente:
```bash
docker-compose up -d
```
4. Para rodar o SeedClientes diretamente, utilize:
```bash
docker-compose exec blackend-api dotnet run --seed SeedClientes
```
- Isso gerará automaticamente 40 clientes (Pessoas Físicas e Jurídicas) no banco de dados.
- Caso o banco já contenha clientes, o Seed não duplicará registros.

### Front-End
- **Angular**: Escolhido por sua robustez, modularidade e grande suporte da comunidade. Permite criar aplicações escaláveis e de fácil manutenção.
- **TypeScript**: Adiciona tipagem estática ao JavaScript, garantindo maior segurança e produtividade durante o desenvolvimento.
- **Bootstrap**: Utilizado para garantir uma interface responsiva e visualmente agradável com pouca configuração adicional.
- **Reactive Forms (Angular Forms)**: Implementados para garantir uma experiência fluida e validação em tempo real nos formulários.
- **HTTPClient (Angular)**: Utilizado para comunicação com a API, garantindo uma integração eficiente e simplificada entre front-end e back-end.

---

## ✅ Estrutura do Projeto
```
/BlackEnd
├── /BlackEnd.API         # API com Controllers
├── /BlackEnd.Application  # CQRS, DTOs, Handlers (Comandos e Queries)
├── /BlackEnd.Domain       # Entidades, Interfaces, Regras de Negócio
├── /BlackEnd.Infrastructure # Repositórios, Contexto de Banco de Dados
├── /BlackEnd.Tests        # Testes Unitários
└── docker-compose.yml     # Configuração Docker para API e Banco de Dados

/cliente-app
├── /src/app               # Código do Front-End Angular
├── /src/app/components    # Componentes de UI
├── /src/app/services      # Serviços para consumo da API
├── /src/app/models        # Modelos de Dados (DTOs)
└── /src/assets            # Recursos estáticos
```

---

## ✅ Demonstração Visual

### ✅ Swagger da Api

![image](https://github.com/user-attachments/assets/2f8aa977-aa95-46cd-b8f9-f46de9d06942)


### ✅ Tela de Listagem de Clientes
- Exibe todos os clientes cadastrados com opção de editar e excluir.

![image](https://github.com/user-attachments/assets/f2b8d82b-fd9d-4a31-9b52-c48b459ea445)


### ✅ Tela de Cadastro de Cliente
- Permite cadastrar um novo cliente (Pessoa Física ou Jurídica).
- Validações em tempo real com Reactive Forms.

![image](https://github.com/user-attachments/assets/5a7ef36b-0de1-40db-8cfa-8692d73c550d)


### ✅ Tela de Edição de Cliente
- Permite editar os dados de um cliente já cadastrado.
- Validações automáticas garantem a integridade dos dados.

![image](https://github.com/user-attachments/assets/83d1e78f-8d96-40c3-be33-bf00f6e7b14c)


- ### ✅ Tela de MySql
- Dados Gerados pelo Seed
- 
![image](https://github.com/user-attachments/assets/0f49240c-8982-4ca7-991d-25857050106d)


### ✅ Test Explore
- Aplicação dos teste Unitário
![image](https://github.com/user-attachments/assets/d487a2a3-e865-4639-8d05-878a1f95dd72)

