
# EcoHome.AuthService.API

## Visão Geral
EcoHome.AuthService.API é um microsserviço desenvolvido para gerenciar usuários, dispositivos e logs de consumo de energia no projeto EcoHome. A solução foi projetada utilizando .NET Core, seguindo os princípios de arquitetura limpa (Clean Architecture) e boas práticas de desenvolvimento, com integração de predição de consumo energético utilizando ML.NET.

## Tecnologias Utilizadas
- **.NET Core 8.0**
- **Entity Framework Core 8.0.3**
- **Oracle Database**
- **ML.NET** para aprendizado de máquina
- **Swagger** para documentação de APIs
- **xUnit** para testes automatizados

## Estrutura do Projeto
- **Presentation (API)**: Contém os controllers e configuração da API.
- **Application**: Contém a lógica de negócios e serviços.
- **Domain**: Define entidades e interfaces do domínio.
- **Infrastructure**: Implementa repositórios e configurações do banco de dados.
- **Tests**: Contém testes unitários e de integração.

## Funcionalidades
1. Cadastro e autenticação de usuários.
2. Gerenciamento de dispositivos vinculados aos usuários.
3. Registro de logs de consumo energético.
4. Predição de consumo utilizando ML.NET.

## Endpoints Principais
### Usuários
- `POST /api/User`: Cria um novo usuário.
- `GET /api/User/{id}`: Obtém um usuário pelo ID.

### Dispositivos
- `POST /api/Device`: Adiciona um dispositivo.
- `GET /api/Device/{id}`: Obtém um dispositivo pelo ID.

### Logs de Consumo
- `POST /api/ConsumptionLog`: Adiciona um log de consumo.
- `GET /api/ConsumptionLog/device/{deviceId}`: Obtém logs de um dispositivo.

### Predições
- `POST /api/ConsumptionLog/predict`: Prediz o consumo energético com base nos dados fornecidos.

## Configuração
1. Configure a string de conexão no `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "User Id=USER;Password=PASSWORD;Data Source=SERVER"
   }
   ```
2. Restaure os pacotes:
   ```bash
   dotnet restore
   ```
3. Aplique as migrações:
   ```bash
   dotnet ef database update
   ```
4. Execute o projeto:
   ```bash
   dotnet run --project EcoHome.AuthService.API
   ```

## Testes
Os testes foram implementados utilizando xUnit. Para executar:
```bash
dotnet test
```

## Estrutura de Pastas
```plaintext
├── EcoHome.AuthService.API
├── EcoHome.AuthService.Application
├── EcoHome.AuthService.Domain
├── EcoHome.AuthService.Infrastructure.Data
├── EcoHome.AuthService.Infrastructure.IoC
├── EcoHome.AuthService.Tests
```

## Contribuição
1. Faça um fork do repositório.
2. Crie um branch para sua feature:
   ```bash
   git checkout -b minha-feature
   ```
3. Envie um pull request após os testes.

## Licença
Este projeto é licenciado sob a MIT License.
