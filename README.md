# Teste_Data_System

---

## Componentes utilizados na implementação do código

- Visual Studio 2019  
- .NEt 5  
- SQL Server 2022  

---

## Instruções para rodar a aplicação

1. Crie um banco no SQL de nome DataSystem  
2. Abra o projeto TesteDataSystem no VisualStudio  
3. Modifique a string de conexão de acordo com suas configurações  
   A string de conexão está no arquivo appsettings.json que se encontra  
no projeto TesteDataSystem.Presentation.  
   "DefaultConnection": "Server=BRUNO;DataBase=DataSystem;User Id=sa;Password=2991;"  
   Modifique o Server, User ID e Password de acordo com suas configurações  
4. Suba o banco de dados para SQL  
   No VisualStudio clique em Ferramentas -> Gerenciador de Pacotes Nuget -> Console do Gerenciador de Pacotes  
   No Cosole que foi aberto, selecione o projeto TesteDataSystem.Infrastructure como Projeto padrão  
   Execute o comando update-database  
5. Execute a solução TesteDataSystem  
6. Abra o projeto FrontTestDataSystem no VisualStudio  
7. Execute a solução FrontTestDataSystem  
8. A aplicação apresenta duas abas Registrar e Pesquisar  
   A aba Registrar cria e atualiza as tarefas  
   A aba Pesquisar lista todas tarefas, filtra as tarefas por status, exclui as tarefas selecionadas e edita as  
tarefas selecionadas (enviando para a aba Registrar).  
