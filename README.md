# 🚗 Sistema de Gerenciamento de Veículos no Pátio

Sistema completo para controle e gerenciamento de veículos em um Pátio, desenvolvido com ASP.NET Core e Angular.

## 📋 Sobre o Projeto

Sistema que permite o controle de entrada e saída de veículos, cálculo automático de valores, e geração de relatórios gerenciais para estacionamentos.
O sistema calcula o valor total com base em dois valores: o valor da primeira hora e o das demais horas:
- Caso o veículo tenha ficado estacionado por um período menor ou igual a uma hora, ele irá pagar o valor da primeira hora,
- Caso o veículo tenha ficado mais do que uma hora, as demais horas são calculadas com um arredondamento: a partir de 30 minutos, considera-se mais uma hora;
  
Ou seja: caso o veículo tenha estacionado por 1h20, ele apenas pagará pela primeira hora. Caso tenha ficado mais (ex.: 1h35), ele irá pagar por duas horas.

### ✨ Funcionalidades

- 📝 Cadastro de veículos (placa, modelo, modelo)
- 🚪 Controle de entrada e saída do pátio
- 💰 Cálculo automático de valores baseado no tempo de permanência
- 📊 Relatórios gerenciais:
  - Faturamento por período (7 ou 30 dias)
- ✅ Validação de placas duplicadas
- 🔍 Verificação de veículos no pátio

## 🛠️ Tecnologias Utilizadas

### Backend
- **ASP.NET Core 9** - Framework web
- **Entity Framework Core** - ORM
- **SQLite** - Banco de dados
- **AutoMapper** - Mapeamento de objetos
- **Clean Architecture** - Arquitetura em camadas

### Frontend
- **Angular 21** - Framework frontend
- **TypeScript** - Linguagem
- **Bootstrap 5** - Framework CSS
- **SCSS** - Pré-processador CSS

## 🚀 Como Executar

### Pré-requisitos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js 25+](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)

### Backend
```bash
# Clone o repositório
git clone https://github.com/lais-goncalves/PatioVeiculos.git

# Navegue até a pasta do backend
cd PatioVeiculos/Back/

# Restaure as dependências
dotnet restore

# Execute as migrations
dotnet ef database update --project PatioVeiculos.Infrastructure -s PatioVeiculos.Presentation

# Execute o projeto
dotnet run --project PatioVeiculos.Presentation
```

A API estará disponível em `https://localhost:5103` (ou a porta configurada).

### Frontend
```bash
# Navegue até a pasta do frontend
cd PatioVeiculos/Front/PatioVeiculos/

# Instale as dependências
npm install

# Execute o projeto
npm start
```

O frontend estará disponível em `http://localhost:4200`.
