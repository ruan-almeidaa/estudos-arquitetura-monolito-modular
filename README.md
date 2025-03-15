﻿# Desafio: API de Gestão de Tarefas (Monolito Modular)

Este repositório contém um desafio para a implementação de uma API utilizando uma arquitetura **monolítica modular**. O objetivo é criar uma API para gerenciar tarefas, separando as responsabilidades em diferentes módulos.

## 📌 Objetivo
Criar uma API RESTful que permita o gerenciamento de tarefas e usuários, garantindo a modularização e a segregação de responsabilidades.

## 📂 Módulos
A API será dividida em dois módulos principais:

### 🧑‍💻 Módulo de Usuários
Responsável pelo cadastro, autenticação e gerenciamento de usuários.

- **Cadastrar Usuário**: Criar um novo usuário.
- **Autenticação**: Gerar um token JWT para acesso à API.
- **Listar Usuários**: Permite que administradores consultem todos os usuários cadastrados.

### ✅ Módulo de Tarefas
Responsável pelo CRUD de tarefas.

- **Criar Tarefa**: Criar uma nova tarefa associada a um usuário.
- **Listar Tarefas**: Exibir todas as tarefas cadastradas.
- **Editar Tarefa**: Atualizar os dados de uma tarefa existente.
- **Excluir Tarefa**: Remover uma tarefa.

## 📌 Estrutura do Banco de Dados

### 🧑‍💻 Tabela: Usuários
| Campo       | Tipo        | Descrição              |
|------------|------------|--------------------------|
| `Id`       | int (PK)   | Identificador único    |
| `Nome`     | string     | Nome do usuário         |
| `Email`    | string     | Email do usuário (unique) |
| `Senha`    | string     | Senha criptografada      |
| `DataCadastro` | datetime | Data de criação da conta |

### ✅ Tabela: Tarefas
| Campo         | Tipo        | Descrição                    |
|--------------|------------|------------------------------|
| `Id`         | int (PK)   | Identificador único         |
| `Titulo`     | string     | Título da tarefa            |
| `Descricao`  | string     | Detalhes da tarefa          |
| `DataConclusao` | datetime | Data limite para conclusão  |
| `Status`     | enum       | Estado da tarefa (Pendente, Em andamento, Concluída) |
| `UsuarioId`  | int (FK)   | ID do usuário dono da tarefa |

## 🔒 Autenticação e Controle de Acesso
- A autenticação será baseada em **JWT (JSON Web Token)**.
- Os usuários serão divididos em dois perfis:
  - **Administrador**: Pode cadastrar e listar usuários.
  - **Usuário Comum**: Pode criar, listar, editar e excluir suas próprias tarefas.

## 🚀 Endpoints

### 🧑‍💻 Usuários
| Método | Rota                  | Descrição                           | Autenticação |
|---------|-----------------------|---------------------------------|-------------|
| POST    | `/api/usuarios`        | Cadastrar um novo usuário      | Não        |
| POST    | `/api/usuarios/login`  | Realizar login e obter token JWT | Não        |
| GET     | `/api/usuarios`        | Listar todos os usuários        | Sim (Admin) |

### ✅ Tarefas
| Método | Rota                  | Descrição                           | Autenticação |
|---------|-----------------------|---------------------------------|-------------|
| POST    | `/api/tarefas`        | Criar uma nova tarefa          | Sim        |
| GET     | `/api/tarefas`        | Listar todas as tarefas        | Sim        |
| PUT     | `/api/tarefas/{id}`   | Editar uma tarefa existente    | Sim        |
| DELETE  | `/api/tarefas/{id}`   | Excluir uma tarefa             | Sim        |

## 🔧 Tecnologias Utilizadas
- .NET 8
- Entity Framework Core
- ASP.NET Core Web API
- JWT para autenticação
- SQL Server ou SQLite para armazenamento

## 🎯 Desafios Extras
- Implementar roles no JWT para diferenciar "Administrador" e "Usuário Comum".
- Criar testes unitários para garantir a qualidade da API.
- Implementar logs para rastrear a execução da API.

## 📜 Licença
Este projeto é open-source e pode ser utilizado para estudos e melhorias.

---
💡 **Dica:** Para iniciar, foque na estruturação modular da API e na separação das responsabilidades dentro do monolito! 🚀

