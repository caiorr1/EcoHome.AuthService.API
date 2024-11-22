
# EcoHome.AuthService.API

## Visão Geral do Projeto

EcoHome.AuthService.API é um serviço desenvolvido para oferecer funcionalidades de autenticação e gerenciamento de usuários, dispositivos e logs de consumo elétrico para o sistema EcoHome, uma solução de monitoramento e gestão de consumo energético em ambientes residenciais. Utilizando .NET e EF Core, o projeto é estruturado em arquitetura limpa e separação clara entre aplicação, domínio e infraestrutura.

## Integrantes

- Caio Ribeiro Rodrigues - RM: 99759
- Guilherme Riofrio Quaglio - RM: 550137
- Elen Cabral - RM: 98790
- Mary Speranzini - RM: 550242
- Eduardo Jablinski - RM: 550975 

## Tecnologias Utilizadas

- **C# / .NET 8**: Para o desenvolvimento da API e serviços.
- **Entity Framework Core**: ORM para manipulação do banco de dados.
- **Oracle**: Banco de dados relacional utilizado.
- **ML.NET**: Usado para treinar um modelo de previsão de consumo elétrico.
- **Swagger**: Documentação interativa da API.

## Funcionalidades

- **Gerenciamento de Usuários**: Cadastro, autenticação e manipulação de dados dos usuários.
- **Gerenciamento de Dispositivos**: Cadastro e atualização dos dispositivos elétricos dos usuários.
- **Logs de Consumo**: Registro do consumo energético de cada dispositivo.
- **Modelo de Previsão de Consumo**: Treinamento e previsão de consumo energético utilizando dados registrados.

## Estrutura do Projeto

- **EcoHome.AuthService.API**: Contém a camada de apresentação da aplicação (Controllers).
- **EcoHome.AuthService.Application**: Contém a lógica de negócio (Services) e classes de suporte, como o preditor ML.NET.
- **EcoHome.AuthService.Domain**: Definições de entidades e interfaces do domínio.
- **EcoHome.AuthService.Infrastructure**: Repositórios que realizam a persistência no banco de dados.

## Setup do Projeto

1. Clone o repositório:

   ```bash
   git clone https://github.com/seu-repositorio/EcoHome.AuthService.API.git
   ```

2. Navegue até a pasta raiz do projeto:

   ```bash
   cd EcoHome.AuthService.API
   ```

3. Configure a string de conexão no arquivo `appsettings.json` com as credenciais do banco Oracle.

4. Restaure as dependências e rode as migrações:

   ```bash
   dotnet restore
   dotnet ef database update
   ```

5. Execute o projeto:

   ```bash
   dotnet run
   ```

## Exemplos de Uso da API

### Cadastro de Usuário

**Endpoint**: `POST /api/users`

**Corpo da Requisição**:

```json
{
  "name": "John Doe",
  "email": "johndoe@example.com",
  "password": "password123"
}
```

**Resposta**:

```json
{
  "id": 1,
  "name": "John Doe",
  "email": "johndoe@example.com",
  "createdAt": "2024-11-17T18:39:07.027155Z"
}
```

### Registro de Consumo de um Dispositivo

**Endpoint**: `POST /api/consumptionlog`

**Corpo da Requisição**:

```json
{
  "deviceId": 1,
  "consumption": 150.5,
  "timestamp": "2024-11-22T19:23:12.1809622Z"
}
```

**Resposta**: Status `204 No Content`.

### Treinamento do Modelo de Previsão

**Endpoint**: `POST /api/consumptionlog/train`

**Resposta**:

```plaintext
"Modelo treinado e salvo com sucesso."
```

### Previsão de Consumo Futuro

**Endpoint**: `GET /api/consumptionlog/predict/{timestamp}`

- **Parâmetro**: `timestamp` - Número de dias no futuro.

**Exemplo de Requisição**: `GET /api/consumptionlog/predict/5`

**Resposta**:

```json
{
  "timestamp": "2024-11-25T20:39:14.4976912Z",
  "predictedConsumption": 0.48455045
}
```

## Testes

O projeto conta com testes unitários e de integração para garantir a qualidade do código e o correto funcionamento dos componentes. Os testes podem ser executados com o seguinte comando:

```bash
dotnet test
```

### Principais Testes

- **FullFlowIntegrationTests**: Testa a criação de um usuário e a associação de dispositivos.
- **ConsumptionLogRepositoryTests**: Testa as operações de criação e consulta de logs de consumo.

## Melhorias Futuras

- **Autenticação via OAuth**: Implementar um mecanismo de autenticação mais robusto.
- **Dashboard para Monitoramento**: Uma interface visual para monitorar o consumo energético em tempo real.
- **Alertas Customizáveis**: Permitir que os usuários configurem alertas para consumo excessivo.

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.