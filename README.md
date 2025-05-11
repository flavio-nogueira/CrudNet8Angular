
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
- O método SeedClientes permite popular automaticamente o banco de dados com 20 registros de clientes, respeitando as regras de negócio:
  - Apenas um cadastro por CPF/CNPJ e E-mail.
  - Pessoas Físicas com idade mínima de 18 anos.
  - Pessoas Jurídicas com Inscrição Estadual ou Isenção de IE.
  - Esse método é útil para testes e desenvolvimento, garantindo um conjunto inicial de dados.

### Front-End
- **Angular**: Escolhido por sua robustez, modularidade e grande suporte da comunidade. Permite criar aplicações escaláveis e de fácil manutenção.
- **TypeScript**: Adiciona tipagem estática ao JavaScript, garantindo maior segurança e produtividade durante o desenvolvimento.
- **Bootstrap**: Utilizado para garantir uma interface responsiva e visualmente agradável com pouca configuração adicional.
- **Reactive Forms (Angular Forms)**: Implementados para garantir uma experiência fluida e validação em tempo real nos formulários.
- **HTTPClient (Angular)**: Utilizado para comunicação com a API, garantindo uma integração eficiente e simplificada entre front-end e back-end.

---

## ✅ <span style="color:blue">Estrutura do Projeto</span>
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
