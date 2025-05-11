
# BlackEnd - API de Cadastro de Clientes

## 🚀 Visão Geral
Esta é uma API de Cadastro de Clientes desenvolvida com .NET 8.0 e MySQL, seguindo os princípios de DDD (Domain-Driven Design), CQRS (Command Query Responsibility Segregation) e Event Sourcing.

---

## ✅ Tecnologias Utilizadas
- **.NET 8.0 (Web API)**
- **MySQL** (Banco de Dados via Docker Compose)
- **CQRS e Event Sourcing**
- **FluentValidation** (Validações de Entrada)
- **xUnit e Moq** (Testes Unitários)
- **AutoMapper** (Mapeamento de Objetos)
- **Docker e Docker Compose**

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
```

---

## ✅ Como Executar o Projeto
1. **Clone o Repositório:**
   ```bash
   git clone <url-do-repositorio>
   cd BlackEnd
   ```

2. **Execute o Docker Compose:**
   ```bash
   docker-compose up -d
   ```

3. **Acesse a API:**
   - URL da API: `http://localhost:5000`
   - Documentação (Swagger): `http://localhost:5000/swagger`

---

## ✅ Testes Unitários
- O projeto possui testes unitários implementados com xUnit e Moq.
- Para rodar os testes:
  ```bash
  dotnet test
  ```

---

## ✅ Docker Compose
O Docker Compose é responsável por levantar a API e o banco de dados MySQL:
```yaml
version: '3.8'
services:
  blackend-api:
    build:
      context: .
      dockerfile: ./BlackEnd.API/Dockerfile
    ports:
      - "5000:80"
      - "5001:443"
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      ConnectionStrings__DefaultConnection: "Server=blackend-db;Database=BlackEndDb;User=root;Password=root"
    depends_on:
      - blackend-db

  blackend-db:
    image: mysql:8.0
    environment:
      MYSQL_ROOT_PASSWORD: root
      MYSQL_DATABASE: BlackEndDb
    ports:
      - "3306:3306"
    volumes:
      - ./data/db:/var/lib/mysql
```

---

## ✅ Como Contribuir
1. Crie uma branch a partir da `main`.
2. Faça suas alterações.
3. Abra um Pull Request (PR).
