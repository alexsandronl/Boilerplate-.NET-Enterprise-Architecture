# Onboarding Rápido - Boilerplate .NET Enterprise Architecture

Bem-vindo(a) ao projeto! Siga este passo a passo para começar a desenvolver rapidamente:

---

## 1. Pré-requisitos
- [.NET 8+](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [Git](https://git-scm.com/)

## 2. Clonar o repositório
```bash
git clone https://github.com/SEU_USUARIO/Boilerplate-.NET-Enterprise-Architecture.git
cd Boilerplate-.NET-Enterprise-Architecture
```

## 3. Configurar variáveis de ambiente
- Copie `.env.example` para `.env` e ajuste as variáveis conforme necessário
- Ou use [User-Secrets](https://learn.microsoft.com/aspnet/core/security/app-secrets)

## 4. Rodar a aplicação
- **Local:**
  ```bash
  dotnet restore
  cd src/Api
  dotnet run
  ```
- **Via Docker:**
  ```bash
  docker-compose -f docker/docker-compose.yml up --build
  ```

Acesse o Swagger: [http://localhost:5000/swagger](http://localhost:5000/swagger)

## 5. Testar a API
- Veja exemplos de requests em [`Docs/Examples.http`](Examples.http)
- Teste endpoints como `/api/orders`, `/auth/login`, `/health`, `/api/localizationdemo/hello`

## 6. Executar testes automatizados
```bash
dotnet test
```

## 7. Estrutura do Projeto
- Veja o README.md para detalhes de arquitetura, diagramas e exemplos
- Principais pastas:
  - `src/Api`: API ASP.NET Core
  - `src/Application`: CQRS, UseCases
  - `src/Domain`: Entidades, Eventos
  - `src/Infrastructure`: EF Core, EventStore, Email, Service Bus
  - `tests/`: Testes unitários e de integração

## 8. Dicas para Customização
- Para adicionar um novo domínio, crie a entidade em `Domain`, comandos/handlers em `Application` e endpoints em `Api`
- Para novos eventos, siga o padrão de Event Sourcing já implementado
- Para integrações, use as abstrações e exemplos prontos (RabbitMQ, Email, etc)

## 9. Suporte
- Consulte os comentários no código e exemplos em `Docs/`
- Dúvidas? Abra uma issue ou discuta no time!

---

> **Boas-vindas!**
> O projeto está pronto para produção, CI/CD, cloud e onboarding de times sêniores. 