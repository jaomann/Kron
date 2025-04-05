# KronApi - Sistema de Gerenciamento de Serviços

KronApi é uma API RESTful desenvolvida em .NET Core para gerenciamento de serviços e agendamentos. O sistema permite que empresas gerenciem seus serviços, funcionários e agenda de forma eficiente.

## 📑 Índice

- [Tecnologias](#-tecnologias)
- [Pré-requisitos](#-pré-requisitos)
- [Estrutura do Projeto](#️-estrutura-do-projeto)
- [Instalação](#-instalação)
- [Autenticação](#-autenticação)
- [Endpoints](#-endpoints-principais)
- [Configuração](#-configuração)
- [Cache](#-cache)
- [Segurança](#-segurança)
- [Contribuição](#-contribuição)
- [Funcionalidades Futuras](#-funcionalidades-futuras)
- [Suporte](#-suporte)

## 🚀 Tecnologias

- .NET Core 8.0
- MySQL 8.0
- Redis (Cache)
- Docker & Docker Compose
- Nginx (Proxy Reverso)
- BCrypt.NET (Criptografia)
- Entity Framework Core
- Minimal APIs

## 📋 Pré-requisitos

- Docker e Docker Compose
- .NET SDK 8.0 (para desenvolvimento)
- IDE (recomendado: Visual Studio Code ou Visual Studio)
- Git

## 🛠️ Estrutura do Projeto

```
📦 KronApi (Solução)
├── 📂 .github/                    # Configurações do GitHub Actions
├── 📂 KronApi/                    # Projeto principal
│   ├── 📂 Core/                   # Núcleo da aplicação
│   │   ├── 📂 Entities/          # Modelos de domínio
│   │   │   ├── User.cs           # Usuário do sistema
│   │   │   ├── Company.cs        # Empresa
│   │   │   ├── Week.cs           # Programação semanal
│   │   │   ├── Day.cs            # Agendamentos diários
│   │   │   └── Service.cs        # Serviços
│   │   ├── 📂 Enums/             # Enumerações do sistema
│   │   └── 📂 Contracts/         # Interfaces e contratos
│   │       ├── 📂 Repository/    # Contratos dos repositórios
│   │       └── 📂 Service/       # Contratos dos serviços
│   ├── 📂 Infrastructure/         # Infraestrutura (recomendado)
│   │   ├── 📂 Data/              # Acesso a dados
│   │   ├── 📂 Cache/             # Implementação do Redis
│   │   └── 📂 Email/             # Serviços de email
│   ├── 📂 Repository/            # Implementação dos repositórios
│   │   ├── 📂 Database/          # Contexto e configurações EF
│   │   └── 📂 Configuration/     # Mapeamentos das entidades
│   ├── 📂 Services/              # Lógica de negócios
│   │   ├── 📂 Auth/             # Serviços de autenticação
│   │   ├── 📂 Company/          # Serviços de empresa
│   │   └── 📂 User/             # Serviços de usuário
│   ├── 📂 Models/                # DTOs e ViewModels
│   │   ├── 📂 Requests/         # Modelos de requisição
│   │   └── 📂 Responses/        # Modelos de resposta
│   ├── 📂 Extensions/            # Extensões e helpers
│   ├── 📂 Middleware/            # Middlewares personalizados
│   └── 📂 Configuration/         # Configurações da aplicação
├── 📂 KronFront/                 # Frontend da aplicação
├── 📂 nginx/                     # Configurações do Nginx
└── 📂 mysql/                     # Scripts e configs do MySQL

📄 Arquivos Principais
├── Program.cs                    # Ponto de entrada e configuração
├── appsettings.json             # Configurações da aplicação
├── Dockerfile                    # Configuração do container
└── docker-compose.yml           # Orquestração dos serviços
```

### Recomendações de Estrutura

1. **Separação de Responsabilidades**:
   - Cada camada deve ter uma responsabilidade única
   - Evite dependências circulares entre camadas
   - Mantenha a direção do fluxo de dependência: Controllers → Services → Repositories

2. **Organização de Serviços**:
   - Agrupe serviços relacionados em namespaces
   - Use injeção de dependência
   - Implemente interfaces para todos os serviços

3. **Padrões de Projeto**:
   - Repository Pattern para acesso a dados
   - Unit of Work para transações
   - Factory Method para criação de objetos complexos
   - Builder Pattern para DTOs complexos

4. **Boas Práticas**:
   - Use pastas específicas para cada tipo de modelo (Request/Response)
   - Mantenha middlewares em pasta separada
   - Centralize configurações em arquivos específicos
   - Use constants para strings e valores mágicos

5. **Testes** (A implementar):
   ```
   📂 Tests/
   ├── 📂 Unit/                   # Testes unitários
   ├── 📂 Integration/            # Testes de integração
   └── 📂 E2E/                    # Testes end-to-end
   ```

### Entidades Principais

- **Company**: 
  - Empresa prestadora de serviços
  - Possui CNPJ, nome e proprietário
  - Relacionada com usuários e agenda

- **User**: 
  - Usuários do sistema (funcionários)
  - Autenticação e permissões
  - Vinculado a uma empresa

- **Week**: 
  - Programação semanal
  - Controle de horas
  - Vinculada a uma empresa

- **Day**: 
  - Agendamentos diários
  - Controle de serviços
  - Parte de uma semana

- **Service**: 
  - Serviços agendados
  - Informações do cliente
  - Duração e tipo

- **Address**: 
  - Endereço da empresa
  - Informações de localização

## 🚀 Instalação

1. Clone o repositório:
```bash
git clone [url-do-repositorio]
```

2. Navegue até a pasta do projeto:
```bash
cd KronApi
```

3. Configure as variáveis de ambiente (opcional):
```bash
cp .env.example .env
# Edite o arquivo .env com suas configurações
```

4. Execute com Docker Compose:
```bash
docker-compose up --build
```

5. Verifique a instalação:
```bash
curl http://localhost/api/health
```

O sistema estará disponível em:
- API: http://localhost/api
- Swagger: http://localhost/api/swagger
- Health Check: http://localhost/api/health

## 🔑 Autenticação

O sistema utiliza um fluxo de autenticação em três etapas:

1. **Verificação de Email**
```http
POST /api/auth/check-email
Content-Type: application/json

{
    "nome": "João",
    "sobrenome": "Silva",
    "email": "joao@exemplo.com"
}
```
Resposta:
```json
{
    "exists": false,
    "message": "User not found"
}
```

2. **Verificação de Senha**
```http
POST /api/auth/verify-password
Content-Type: application/json

{
    "email": "joao@exemplo.com",
    "password": "senha123"
}
```
Resposta:
```json
{
    "message": "Confirmation email sent",
    "token": "confirmation-token"
}
```

3. **Confirmação de Email**
```http
POST /api/auth/register/confirm?email=joao@exemplo.com&token=confirmation-token
```
Resposta:
```json
{
    "message": "Registration completed successfully"
}
```

## 📡 Endpoints Principais

### Empresas
- `GET /company?id={guid}` - Obtém detalhes de uma empresa
- `POST /company` - Cria uma nova empresa
  ```json
  {
    "name": "Empresa Exemplo",
    "cnpj": "12345678901234",
    "address": {
      "street": "Rua Exemplo",
      "number": "123",
      "city": "Cidade"
    }
  }
  ```
- `PUT /company` - Atualiza uma empresa
- `DELETE /company?id={guid}` - Remove uma empresa

### Usuários
- `GET /user?id={guid}` - Obtém detalhes de um usuário
- `POST /user` - Cria um novo usuário
- `PUT /user` - Atualiza um usuário
- `DELETE /user?id={guid}` - Remove um usuário

### Autenticação
- `POST /auth/check-email` - Verifica existência do email
- `POST /auth/verify-password` - Valida credenciais
- `POST /auth/register` - Inicia registro de novo usuário
- `POST /auth/register/confirm` - Confirma registro

## 🔧 Configuração

### Variáveis de Ambiente
```env
# Database
ConnectionStrings__cnMySql=Server=db;Database=krondb;User=root;Password=senharoot

# Redis
Redis__ConnectionString=redis:6379

# Email (a ser implementado)
Email__Host=smtp.exemplo.com
Email__Port=587
Email__Username=seu-email@exemplo.com
Email__Password=sua-senha
```

### Docker Compose
O projeto inclui:
- **MySQL**: Banco de dados principal
  - Porta: 3306
  - Volume persistente

- **Redis**: Cache
  - Porta: 6379
  - Dados temporários

- **Nginx**: Proxy Reverso
  - Porta: 80
  - Roteamento de requisições

- **API**: Aplicação principal
  - Porta interna: 8080
  - Healthcheck configurado

## 📦 Cache

O sistema utiliza Redis para:
- Cache de dados temporários
- Armazenamento de tokens de confirmação (24h de expiração)
- Dados de registro pendentes
- Chaves no formato: `registration:{email}:{token}`

## 🔐 Segurança

- **Senhas**:
  - Hasheadas com BCrypt
  - Salt único por senha
  - Nunca armazenadas em texto plano

- **Autenticação**:
  - Confirmação por email
  - Tokens temporários
  - Validação em múltiplas etapas

- **Infraestrutura**:
  - CORS configurado
  - Proxy reverso com Nginx
  - Conexões seguras

## 🤝 Contribuição

1. Faça um Fork do projeto
2. Crie uma Branch para sua Feature
   ```bash
   git checkout -b feature/AmazingFeature
   ```
3. Commit suas mudanças
   ```bash
   git commit -m 'Add some AmazingFeature'
   ```
4. Push para a Branch
   ```bash
   git push origin feature/AmazingFeature
   ```
5. Abra um Pull Request

### Padrões de Código
- Use PascalCase para classes e métodos
- Use camelCase para variáveis
- Adicione comentários quando necessário
- Siga os princípios SOLID

## 📝 Licença

Este projeto está sob a licença [MIT](https://choosealicense.com/licenses/mit/).

## ✨ Funcionalidades Futuras

- [ ] Implementação de JWT para autenticação
- [ ] Dashboard de métricas
- [ ] Notificações em tempo real
- [ ] Relatórios personalizados
- [ ] App mobile
- [ ] Integração com sistemas de pagamento
- [ ] Sistema de avaliações
- [ ] API de geolocalização
- [ ] Backup automático
- [ ] Logs centralizados

## 🐛 Problemas Conhecidos

- Necessário implementar serviço de email
- Cache precisa de mecanismo de limpeza periódica
- Falta implementar recuperação de senha

## 📞 Suporte

Para suporte:
- Email: [seu-email@exemplo.com]
- Issues: Utilize o sistema de issues do GitHub
- Discord: [link-do-servidor]

## 📊 Status do Projeto

![GitHub last commit](https://img.shields.io/github/last-commit/seu-usuario/KronApi)
![GitHub issues](https://img.shields.io/github/issues/seu-usuario/KronApi) 