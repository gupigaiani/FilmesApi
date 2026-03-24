# FilmesApi

## 🧾 Descrição do Projeto

O projeto FilmesApi é uma aplicação backend em .NET 10 para controle de filmes, cinemas, endereços e sessões, permitindo que os usuários gerenciem um catálogo de filmes e suas exibições em cinemas de forma organizada.
O projeto utiliza Entity Framework Core com MySQL, seguindo boas práticas de arquitetura com mapeamento de objetos via AutoMapper.

## 🚀 Funcionalidades

- CRUD completo de filmes (FilmeController).
- CRUD completo de cinemas (CinemaController).
- CRUD completo de endereços (EnderecoController).
- CRUD de sessões vinculadas a filmes e cinemas (SessaoController).
- Estrutura organizada com DTOs para criação, leitura e atualização.

## 🛠 Tecnologias

- .NET 10
- MySQL
- Entity Framework Core
- AutoMapper
- Swagger/OpenAPI
- Newtonsoft.Json

## 📁 Estrutura do Projeto

FilmesApi/
- ├── Controllers/                 -> Controladores da API 
- ├── Data/                        -> Contexto do banco e DTOs
- ├── Migrations/                  -> Migrações do Entity Framework
- ├── Models/                      -> Entidades do domínio (Filme, Cinema, Endereco, Sessao)
- ├── Profiles/                    -> Perfis do AutoMapper
- ├── Properties/                  -> Configurações do projeto
- ├── appsettings.json             -> Configurações da aplicação
- ├── Program.cs                   -> Ponto de entrada da aplicação

## ⚙️ Pré-requisitos

- .NET 10 SDK
- MySQL Server
- Ferramenta de testes de API: Postman ou navegador para Swagger
- Git

## 🛠 Instalação e Setup

Configure a connection string no appsettings.Development.json (ou appsettings.json):
```
"ConnectionStrings": {
    "FilmeConnection": "Server=localhost;Port=3306;Database=filme;User=root;Password=root;"
}
```

Crie e atualize o banco de dados:
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Execute a API:
```
dotnet run
```

Acesse a documentação Swagger:
```
http://localhost:5126/swagger
```

## 🧑‍💻 Testando os Endpoints

### Filmes
Criar filme:
```
POST /filme
{
  "titulo": "Título do Filme",
  "genero": "Gênero",
  "duracao": 120
}
```

Listar filmes:
```
GET /filme
```

Obter filme por ID:
```
GET /filme/{id}
```

Atualizar filme:
```
PUT /filme/{id}
{
  "titulo": "Novo Título",
  "genero": "Novo Gênero",
  "duracao": 130
}
```

Deletar filme:
```
DELETE /filme/{id}
```

### Cinemas
Criar cinema:
```
POST /cinema
{
  "nome": "Nome do Cinema",
  "enderecoId": 1
}
```

Listar cinemas (opcional: filtrar por enderecoId):
```
GET /cinema?enderecoId=1
```

Obter cinema por ID:
```
GET /cinema/{id}
```

Atualizar cinema:
```
PUT /cinema/{id}
{
  "nome": "Novo Nome",
  "enderecoId": 1
}
```

Deletar cinema:
```
DELETE /cinema/{id}
```

### Endereços
Criar endereço:
```
POST /endereco
{
  "logradouro": "Rua Exemplo",
  "numero": "123",
  "bairro": "Centro",
  "cep": "12345-678",
  "cidade": "Cidade",
  "estado": "Estado"
}
```

Listar endereços:
```
GET /endereco
```

Obter endereço por ID:
```
GET /endereco/{id}
```

Atualizar endereço:
```
PUT /endereco/{id}
{
  "logradouro": "Nova Rua",
  "numero": "456",
  "bairro": "Novo Bairro",
  "cep": "98765-432",
  "cidade": "Nova Cidade",
  "estado": "Novo Estado"
}
```

Deletar endereço:
```
DELETE /endereco/{id}
```

### Sessões
Criar sessão:
```
POST /sessao
{
  "filmeId": 1,
  "cinemaId": 1,
  "horarioDeEncerramento": "2026-03-24T22:00:00"
}
```

Listar sessões:
```
GET /sessao
```

Obter sessão por IDs:
```
GET /sessao/{filmeId}/{cinemaId}
```

## ⚠️ Tratamento de Erros

A API retorna códigos HTTP apropriados para cada situação:

- 200 OK → Sucesso
- 201 Created → Recurso criado
- 400 Bad Request → Erro de validação
- 404 Not Found → Recurso não encontrado