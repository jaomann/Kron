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
├── 📂 Core/                      # Núcleo da aplicação
│   ├── 📂 Contracts/            # Interfaces e contratos
│   │   ├── 📂 Repository/       # Contratos dos repositórios
│   │   └── 📂 Service/         # Contratos dos serviços
│   └── 📂 Entities/            # Modelos de domínio
│       ├── User.cs             # Usuário do sistema
│       ├── Company.cs          # Empresa
│       ├── Week.cs             # Programação semanal
│       ├── Day.cs              # Agendamentos diários
│       ├── Service.cs          # Serviços
│       └── Address.cs          # Endereço
├── 📂 Infrastructure/          # Infraestrutura
│   ├── 📂 Cache/              # Implementação do Redis
│   └── 📂 Email/              # Serviços de email
├── 📂 Repository/             # Implementação dos repositórios
│   ├── 📂 Database/          # Contexto e configurações EF
│   └── UserRepository.cs, CompanyRepository.cs, etc.
├── 📂 Services/               # Serviços da aplicação
│   ├── UserService.cs        # Serviço de usuários
│   ├── CompanyService.cs     # Serviço de empresas
│   └── WeekService.cs, etc.  # Outros serviços
├── 📂 Models/                 # DTOs
│   ├── 📂 UserDTO/           # DTOs relacionados a usuários
│   └── 📂 CompanyDTO/        # DTOs relacionados a empresas
├── 📂 Extensions/            # Extensões e configurações
│   └── ServiceCollectionExtensions.cs
└── Program.cs                # Ponto de entrada e configuração da API
```

### Principais Componentes

1. **Core/**
   - Contém as entidades de domínio e contratos
   - Define as interfaces dos repositórios e serviços
   - Mantém a lógica de negócio isolada

2. **Infrastructure/**
   - Implementações de serviços externos
   - Cache com Redis
   - Serviço de email

3. **Repository/**
   - Implementação do padrão Repository
   - Acesso a dados via Entity Framework
   - Contexto do banco de dados

4. **Services/**
   - Implementação dos serviços da aplicação
   - Lógica de negócio
   - Orquestração entre repositórios

5. **Models/**
   - DTOs para transferência de dados
   - Separação por domínio (User, Company, etc.)

6. **Extensions/**
   - Extensões para configuração da aplicação
   - Registro de serviços e dependências

### Padrões Utilizados

- **Dependency Injection**
  - Registro de serviços via extensões
  - Injeção de dependências nos controllers

- **Repository Pattern**
  - Abstração do acesso a dados
  - Interfaces definidas em Core/Contracts

- **Service Layer**
  - Orquestração da lógica de negócio
  - Separação de responsabilidades

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


## ✨ Funcionalidades Futuras

- [ ] Implementação de JWT para autenticação
- [ ] Validação por email
- [ ] Dashboard de métricas
- [ ] Notificações em tempo real
- [ ] Relatórios personalizados
- [ ] App mobile
- [ ] Integração com sistemas de pagamento
- [ ] Integração com emissão fiscal dos serviços prestados
- [ ] Sistema de avaliações
- [ ] API de geolocalização
- [ ] Backup automático
- [ ] Logs centralizados

## 🐛 Problemas Conhecidos

- Necessário implementar serviço de email
- Cache precisa de mecanismo de limpeza periódica
- Falta implementar recuperação de senha

## 📊 Status do Projeto

![GitHub last commit](https://img.shields.io/github/last-commit/seu-usuario/KronApi)
![GitHub issues](https://img.shields.io/github/issues/seu-usuario/KronApi) 
