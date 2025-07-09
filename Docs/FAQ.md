# FAQ Técnico - Boilerplate .NET Enterprise Architecture

## 1. Arquitetura

**Q:** Qual padrão de arquitetura está sendo usado?
**A:** Clean Architecture, DDD, CQRS e Event Sourcing, com separação clara entre camadas (Domain, Application, Infrastructure, API, Shared).

**Q:** Como adicionar um novo domínio?
**A:** Crie a entidade em `Domain`, comandos/handlers em `Application` e endpoints em `Api`. Siga os exemplos de Order.

**Q:** Como funciona o Event Sourcing?
**A:** Eventos de domínio são persistidos no EventStoreDB. Veja exemplos em `Infrastructure/EventSourcing` e handlers de eventos em `Application`.

---

## 2. Build e Execução

**Q:** Como rodar o projeto localmente?
**A:**
```bash
dotnet restore
cd src/Api
dotnet run
```
Ou use Docker:
```bash
docker-compose -f docker/docker-compose.yml up --build
```

**Q:** O build falha, o que fazer?
**A:** Rode `dotnet clean`, `dotnet restore` e `dotnet build`. Verifique se todos os pacotes NuGet estão instalados.

---

## 3. Testes

**Q:** Como executar os testes?
**A:**
```bash
dotnet test
```

**Q:** Como criar um novo teste?
**A:** Adicione um arquivo em `tests/Unit` ou `tests/Integration` e siga os exemplos com xUnit e Moq.

---

## 4. Docker e Banco de Dados

**Q:** Docker não sobe o SQL Server/EventStore/RabbitMQ, o que fazer?
**A:**
- Verifique se o Docker Desktop está rodando
- Libere memória/recursos
- Use `docker-compose down -v` para limpar volumes

**Q:** Como mudar a porta da API?
**A:** Altere em `docker-compose.yml` e/ou `launchSettings.json`.

---

## 5. CI/CD

**Q:** Como funciona o pipeline?
**A:** O workflow do GitHub Actions faz build, testes e deploy (exemplo para Azure). Adapte para AWS/GCP conforme necessário.

**Q:** Como adicionar secrets no pipeline?
**A:** Use o menu Settings > Secrets do GitHub e referencie no workflow.

---

## 6. Integrações

**Q:** Como publicar/consumir mensagens no RabbitMQ?
**A:** Use o serviço `IRabbitMqService` e siga o exemplo do `ServiceBusController`.

**Q:** Como enviar e-mails?
**A:** Use o serviço `IEmailSender` e o endpoint `/api/email/send`.

**Q:** Como customizar políticas de resiliência (Polly)?
**A:** Edite o serviço `HttpResilientClient` em `Infrastructure/Resilience`.

---

## 7. Localization (Multilíngue)

**Q:** Como adicionar um novo idioma?
**A:** Crie um arquivo `.resx` na pasta `Resources` seguindo o padrão do controller. Veja exemplos em `Api/Resources`.

---

## 8. Troubleshooting

**Q:** Porta ocupada?
**A:** Altere a porta no `launchSettings.json` ou `docker-compose.yml`.

**Q:** Erro de conexão com banco?
**A:** Verifique strings de conexão e se o container está rodando.

**Q:** Dúvidas sobre endpoints?
**A:** Veja o Swagger ou `Docs/Examples.http`.

---

## 9. Boas Práticas

- Sempre escreva testes para novos fluxos
- Use comentários claros e humanos
- Prefira injeção de dependência para serviços
- Siga o padrão de CQRS e Event Sourcing para novos domínios

---

> Dúvidas não respondidas? Consulte o README, Docs ou abra uma issue! 