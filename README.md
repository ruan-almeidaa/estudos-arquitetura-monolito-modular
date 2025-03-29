
# Desafio: API de Gestão de Tarefas (Monolito Modular)

Este repositório contém um desafio para a implementação de uma API utilizando uma arquitetura **monolítica modular**. O objetivo é criar uma API para gerenciar tarefas, separando as responsabilidades em diferentes módulos.

## 📌 Objetivo
Criar uma API RESTful que permita o gerenciamento de tarefas e usuários, garantindo a modularização e a segregação de responsabilidades.

## 🔒 Autenticação e Controle de Acesso
- A autenticação será baseada em **JWT (JSON Web Token)**.
- Os usuários serão divididos em dois perfis:
  - **Administrador**: Pode cadastrar, editar, excluir e listar usuários.
  - **Usuário Comum**: Pode cadastrar, editar e excluir sua própria conta. Pode listar, editar e excluir suas próprias tarefas.

## 🔧 Tecnologias Utilizadas
- .NET 8
- Entity Framework Core
- ASP.NET Core Web API
- JWT para autenticação
- SQL Server
- Fluent Validation
- AutoMapper

## 📜 Licença
Este projeto é open-source e pode ser utilizado para estudos e melhorias.

## 📂 Módulos
A API será dividida em dois módulos principais:

### Módulo de Usuários
Responsável pelo cadastro, autenticação e gerenciamento de usuários.

🗃️Tabelas:

Usuarios
| Campo       | Tipo        | Descrição              |
|------------|------------|--------------------------|
| `Id`       | int (PK)   | Identificador único    |
| `Nome`     | string     | Nome do usuário         |
| `Nivel Usuario`    | int | 0 Usuario, 1 Administrador |
| `DataCadastro` | datetime | Data de criação da conta |

Credenciais
| Campo       | Tipo        | Descrição              |
|------------|------------|--------------------------|
| `Id`       | int (PK)   | Identificador único    |
| `UsuarioId`     | int     | Relação com a tabela de Usuarios|
| `Email`    | string     | Email do usuário|
| `Senha` | string | Senha do usuário |

🚀 Endpoints
|Método|Rota|Descrição| Autenticação|
|---------|-----------------------|---------------------------------|-------------|
|GET|`/api/Usuario/BuscarTodos`|Lista todos usuários|Sim, (Admin)|
|DEL|`/api/Usuario/Excluir`|Exclui cadastro do usuário|Sim|
|PUT|`/api/Usuario/Editar`|Edita cadastro do usuário|Sim|
|POST|`/api/Usuario/Criar`|Cria usuário|Não|
|POST|`/api/Usuario/Autenticar`|Faz login do usuário, retornando token|Não|


### Módulo de Tarefas
Responsável pelo CRUD de tarefas.

🗃️Tabelas:

Tarefas
| Campo         | Tipo        | Descrição                    |
|--------------|------------|------------------------------|
| `Id`         | int (PK)   | Identificador único         |
| `Titulo`     | string     | Título da tarefa            |
| `Descricao`  | string     | Detalhes da tarefa          |
| `DataConclusao` | datetime | Data limite para conclusão  |
| `Status`     | enum       | Estado da tarefa (Pendente, Em andamento, Concluída) |
| `UsuarioId`  | int (FK)   | ID do usuário dono da tarefa |



