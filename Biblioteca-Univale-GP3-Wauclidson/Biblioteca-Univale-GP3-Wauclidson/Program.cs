using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

class Program
{
    // Caminho absoluto para o arquivo da logo
    private static string logoPath = @"C:\Users\Waudias\source\repos\ConsoleApp1\ConsoleApp1\bin\Debug\net8.0\A_logo_for_V&W_SOLUCOES_SOFTWARE_HOUSE.png";

    // Link exibido no console (pode ser o mesmo que o caminho)
    private static string LOGO_LINK = logoPath;

    static ArvoreABB arvore = new ArvoreABB();
    static int ultimoIdAdicionado = 0;
    static decimal totalAluguel = 0;

    static List<string> logAluguel = new List<string>();
    static List<string> logRetirada = new List<string>();
    static List<string> logDelecao = new List<string>();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Title = "Sistema de Gerenciamento de Biblioteca";

        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        TransicaoSuave();

        // PRIMEIRO: Introdução com nome do sistema
        MostrarAbertura();

        // DEPOIS: Tela de login
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        TransicaoSuave();
        new Login().Iniciar();

        // CONTINUA O SISTEMA NORMAL
        if (arvore.ContarLivros() == 0)
            CarregarLivrosImportantes();

        while (true)
        {
            ExibirMenu();
        }
    }

    static void MostrarAbertura()
    {
        // Tenta abrir a logo automaticamente
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = logoPath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao abrir a logo: {ex.Message}");
        }

        Console.Clear();
        Console.WriteLine($"Logo Oficial: {LOGO_LINK}\n");

        MostrarTextoCentralizado(
            "============",
            "V&W SOLUÇÕES",
            "============"
        );
        Thread.Sleep(1500);

        MostrarTextoCentralizado(
            "================================",
            "VM ENGINE DEVELOPMENT VERSÃO 1.0",
            "================================"
        );
        Thread.Sleep(1500);

        MostrarTextoCentralizado(
            "======================================",
            "SISTEMA DE GERENCIAMENTO DE BIBLIOTECA",
            "======================================"
        );
        Thread.Sleep(1500);

        Console.Clear();
    }
    class Login
    {
        public void Iniciar()
        {
            Console.Title = "Login - Sistema de Console";
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();

            string senhaCorreta = "vew555";
            string senhaDigitada = "";

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            string[] loginHeader = new string[]
 {
    "╔════════════════════════════════════════════════════════════════════╗",
    "║          BIBLIOTECA UNIVALE - DR. GERALDO VIANNA CRUZ              ║",
    "║         IDENTIFICAÇÃO NECESSÁRIA PARA ENTRAR NO SISTEMA            ║",
    "╚════════════════════════════════════════════════════════════════════╝"
 };
            int meioTela = Console.WindowHeight / 2;
            int topo = meioTela - loginHeader.Length;

            for (int i = 0; i < loginHeader.Length; i++)
            {
                int esquerda = (Console.WindowWidth - loginHeader[i].Length) / 2;
                Console.SetCursorPosition(esquerda, topo + i);
                Console.WriteLine(loginHeader[i]);
            }

            string prompt = "PARA ENTRAR DIGITE A SENHA: ";
            int promptLeft = (Console.WindowWidth - prompt.Length) / 2;
            Console.SetCursorPosition(promptLeft, topo + loginHeader.Length + 2);
            Console.Write(prompt);
            senhaDigitada = Console.ReadLine();

            if (senhaDigitada == senhaCorreta)
            {
                Console.Clear();
                Console.WriteLine("╔══════════════════════════════════════╗");
                Console.WriteLine("║         BEM-VINDO AO SISTEMA!        ║");
                Console.WriteLine("╚══════════════════════════════════════╝");
                Console.WriteLine();

                InicializarSistema();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Senha incorreta. Acesso negado.");
                Console.WriteLine("Pressione ENTER para tentar novamente...");
                Console.ReadLine();
                Iniciar();
            }

            Console.ResetColor();
        }

        private void InicializarSistema()
        {
            Console.WriteLine("Inicializando...");
            Thread.Sleep(1000);
            Console.WriteLine("Carregando módulos...");
            Thread.Sleep(1000);
            Console.WriteLine("Sistema pronto para uso!");
            Thread.Sleep(1000);
        }
    }

    static void MostrarTextoCentralizado(string linha1, string texto, string linha2)
    {
        Console.Clear();
        int meioTela = Console.WindowHeight / 2;
        int esquerda1 = (Console.WindowWidth - linha1.Length) / 2;
        int esquerda2 = (Console.WindowWidth - texto.Length) / 2;
        int esquerda3 = (Console.WindowWidth - linha2.Length) / 2;
        Console.SetCursorPosition(esquerda1, meioTela - 1);
        Console.WriteLine(linha1);
        Console.SetCursorPosition(esquerda2, meioTela);
        Console.WriteLine(texto);
        Console.SetCursorPosition(esquerda3, meioTela + 1);
        Console.WriteLine(linha2);
    }

    static void ExibirMenu()
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        Console.Clear();

        int totalLivros = arvore.ContarLivros();
        string info = $"Inventário: {totalLivros} livro(s) | Último ID adicionado: {ultimoIdAdicionado}";
        int infoLeft = (Console.WindowWidth - info.Length) / 2;
        Console.SetCursorPosition(infoLeft, 2);
        Console.WriteLine(info);

        string[] menu = {
            "╔════════════════════════════════════════════════════════════════════╗",
            "║                   BIBLIOTE UNIVALE - DR. GERALDO VIANNA CRUZ       ║",
            "╠════════════════════════════════════════════════════════════════════╣",
            "║ 1 - Inserir novo livro                                             ║",
            "║ 2 - Buscar por um livro                                            ║",
            "║ 3 - Listar todos os livros                                         ║",
            "║ 4 - Alugar Livro                                                   ║",
            "║ 5 - Deletar Livro                                                  ║",
            "║ 6 - Área Administrativa (Caixa)                                    ║",
            "║ 7 - Créditos                                                       ║",
            "║ 8 - Sair                                                           ║",
            "╚════════════════════════════════════════════════════════════════════╝"
        };

        int verticalOffset = (Console.WindowHeight - menu.Length) / 2;
        for (int i = 0; i < menu.Length; i++)
        {
            int horizontalOffset = (Console.WindowWidth - menu[i].Length) / 2;
            Console.SetCursorPosition(horizontalOffset, verticalOffset + i);
            Console.WriteLine(menu[i]);
        }
        MostrarRodape();

        string prompt = "Escolha uma opção: ";
        int promptLeft = (Console.WindowWidth - prompt.Length) / 2;
        int promptTop = verticalOffset + menu.Length + 2;
        Console.SetCursorPosition(promptLeft, promptTop);
        string opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1": InserirLivro(); break;
            case "2": BuscarPorUmLivro(); break;
            case "3": ListarLivros(); break;
            case "4": AlugarLivro(); break;
            case "5": DeletarLivro(); break;
            case "6": AreaAdministrativa(); break;
            case "7": MostrarCreditos(); break;
            case "8": EncerrarPrograma(); break;
            default: MensagemTemporaria("Opção inválida! Tente novamente."); break;
        }
    }

    static void MostrarRodape()
    {
        string rodape = $"Total de livros: {arvore.ContarLivros()} | {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        int esquerda = (Console.WindowWidth - rodape.Length) / 2;
        int altura = Console.WindowHeight - 2;
        Console.SetCursorPosition(esquerda, altura);
        Console.WriteLine(rodape);
    }

    static void MostrarCreditos()
    {
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.ForegroundColor = ConsoleColor.White;
        TransicaoSuave();

        Console.Clear();
        Console.WriteLine($"Logo Oficial: {LOGO_LINK}\n");

        string[] creditos = {
        "╔════════════════════════════════════════════════════════════════════════════════════╗",
        "║                             CRÉDITOS DO SISTEMA                                    ║",
        "╠════════════════════════════════════════════════════════════════════════════════════╣",
        "║ • Projeto acadêmico da disciplina de Estrutura de Dados                            ║",
        "║ • Professor orientador: Henrique Bianor Freitas Silva                              ║",
        "║ • Universidade: Universidade Vale do Rio Doce (UNIVALE)                            ║",
        "║ • Sistema: Bibliote Univale - Dr. GERALDO VIANNA CRUZ                              ║",
        "║ • Desenvolvido por:                                                                ║",
        "║     - Vitor Manoel Vidal Braz                                                      ║",
        "║       GitHub: https://github.com/vitormanoelvb                                     ║",
        "║                                                                                    ║",
        "║     - Wauclidson Alves Dias                                                        ║",
        "║       GitHub: https://github.com/WAUCLIDSON                                        ║",
        "║                                                                                    ║",
        "║ • Primeiro projeto a usar: VM Engine Development v1.0                              ║",
        "╚════════════════════════════════════════════════════════════════════════════════════╝"
    };

        int meioTela = Console.WindowHeight / 2 - creditos.Length / 2;
        foreach (string linha in creditos)
        {
            int esquerda = (Console.WindowWidth - linha.Length) / 2;
            Console.SetCursorPosition(esquerda, meioTela++);
            Console.WriteLine(linha);
            Thread.Sleep(100);
        }

        AbrirLinks();

        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
        Console.ReadLine();

        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.White;
        TransicaoSuave();
    }

    static void AbrirLinks()
    {
        Console.WriteLine("\nDeseja abrir o GitHub do Vitor? (s/n)");
        if (Console.ReadKey().KeyChar.ToString().ToLower() == "s")
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/vitormanoelvb",
                    UseShellExecute = true
                });
                Console.WriteLine("\nGitHub do Vitor aberto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao abrir o GitHub do Vitor: {ex.Message}");
            }
        }

        Console.WriteLine("\nDeseja abrir o GitHub do Wauclidson? (s/n)");
        if (Console.ReadKey().KeyChar.ToString().ToLower() == "s")
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "https://github.com/WAUCLIDSON",
                    UseShellExecute = true
                });
                Console.WriteLine("\nGitHub do Wauclidson aberto.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nErro ao abrir o GitHub do Wauclidson: {ex.Message}");
            }
        }
    }
    static void EncerrarPrograma()
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        TransicaoSuave();

        Console.Clear();
        Console.WriteLine($"Logo Oficial: {LOGO_LINK}\n");

        string[] mensagem = {
            "=================================================================",
            "Obrigado por utilizar este sistema! Espero que o código tenha",
            "atendido suas expectativas e contribuído para a solução desejada.",
            "================================================================="
        };

        int meioTela = Console.WindowHeight / 2;
        for (int i = 0; i < mensagem.Length; i++)
        {
            int esquerda = (Console.WindowWidth - mensagem[i].Length) / 2;
            Console.SetCursorPosition(esquerda, meioTela - 1 + i);
            Console.WriteLine(mensagem[i]);
        }

        Thread.Sleep(3000);
        Environment.Exit(0);
    }

    static void TransicaoSuave()
    {
        Console.Clear();
        for (int i = 0; i < Console.WindowHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write(new string(' ', Console.WindowWidth));
            Thread.Sleep(5);
        }
        Console.Clear();
    }

    static void MensagemTemporaria(string mensagem)
    {
        Console.WriteLine($"\n{mensagem}");
        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
    }

    // Coleta dados pessoais do cliente
    static string ObterDadosCliente()
    {
        Console.WriteLine("\nPor favor, informe seus dados pessoais.");
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        Console.Write("CPF: ");
        string cpf = Console.ReadLine();
        Console.Write("Telefone: ");
        string telefone = Console.ReadLine();
        return $"Nome: {nome}, CPF: {cpf}, Telefone: {telefone}";
    }

    // Exibe o termo de responsabilidade e solicita aceitação
    static bool AceitarTermoResponsabilidade(string tipoUso)
    {
        Console.WriteLine($"\nTermo de Responsabilidade - {tipoUso}");
        Console.WriteLine("Eu, o cliente, declaro estar ciente dos termos e condições para o uso deste serviço.");
        Console.WriteLine("Ao prosseguir, assumo total responsabilidade pelo uso do livro, seja por aluguel ou retirada para uso presencial.");
        Console.WriteLine("Declaro que os dados pessoais fornecidos são verdadeiros e autorizo o uso destes para controle e inventário.");
        Console.Write("Você aceita os termos? (s/n): ");
        string resposta = Console.ReadLine().Trim().ToLower();
        return resposta == "s";
    }

    // Função para inserir novo livro
    static void InserirLivro()
    {
        Console.Clear();
        Console.WriteLine("=== Inserir Novo Livro ===\n");
        Console.WriteLine($"Último ID adicionado: {ultimoIdAdicionado}\n");

        try
        {
            Livro livro = new Livro();
            Console.Write("ID: ");
            livro.Id = int.Parse(Console.ReadLine());
            if (arvore.Buscar(livro.Id) != null)
            {
                MensagemTemporaria("ID já cadastrado! Insira um ID diferente.");
                return;
            }
            Console.Write("Título: ");
            livro.Titulo = Console.ReadLine();
            Console.Write("Autor: ");
            livro.Autor = Console.ReadLine();
            Console.Write("Ano: ");
            livro.Ano = int.Parse(Console.ReadLine());
            Console.Write("História/Resumo do Livro: ");
            livro.Historia = Console.ReadLine();
            livro.Disponivel = true;
            arvore.Inserir(livro);
            ultimoIdAdicionado = livro.Id;
            MensagemTemporaria("Livro inserido com sucesso!");
        }
        catch
        {
            MensagemTemporaria("Erro ao inserir. Verifique os dados.");
        }
    }

    // Função para buscar por um livro com submenu
    static void BuscarPorUmLivro()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Buscar Por Um Livro ===\n");
            Console.WriteLine("Livros disponíveis:");
            arvore.ListarLivrosDisponiveis();
            Console.WriteLine("\n---------------------------------------------");
            Console.WriteLine("Menu de busca:");
            Console.WriteLine("1 - Buscar por ID");
            Console.WriteLine("2 - Buscar por Título");
            Console.WriteLine("3 - Filtrar por Ano");
            Console.WriteLine("4 - Filtrar por Autor");
            Console.WriteLine("5 - Voltar ao menu principal");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            Livro livro = null;
            switch (opcao)
            {
                case "1":
                    livro = BuscarPorIdDisponivel();
                    break;
                case "2":
                    livro = BuscarPorTitulo();
                    break;
                case "3":
                    livro = FiltrarPorAno();
                    break;
                case "4":
                    livro = FiltrarPorAutor();
                    break;
                case "5":
                    return;
                default:
                    MensagemTemporaria("Opção inválida!");
                    continue;
            }

            if (livro != null)
            {
                Console.WriteLine("\nResultado da busca:");
                Console.WriteLine(livro);
                MenuAjuda(livro);
            }
            else
            {
                MensagemTemporaria("Nenhum livro encontrado com os critérios informados.");
            }
        }
    }

    // Busca por ID (apenas entre os disponíveis)
    static Livro BuscarPorIdDisponivel()
    {
        Console.Write("Digite o ID do livro: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Livro livro = arvore.Buscar(id);
            if (livro != null && livro.Disponivel)
                return livro;
        }
        return null;
    }

    // Busca por Título
    static Livro BuscarPorTitulo()
    {
        Console.Write("Digite parte do título: ");
        string titulo = Console.ReadLine().Trim().ToLower();
        List<Livro> livros = arvore.ObterLivrosEmOrdem().Where(l => l.Disponivel && l.Titulo.ToLower().Contains(titulo)).ToList();
        if (livros.Count > 0)
        {
            Console.WriteLine("\nLivros encontrados:");
            foreach (Livro l in livros)
            {
                Console.WriteLine(l);
                Console.WriteLine("---------------------------------------------");
            }
            Console.Write("Digite o ID do livro desejado: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                return livros.FirstOrDefault(l => l.Id == id);
            }
        }
        return null;
    }

    // Filtrar por Ano
    static Livro FiltrarPorAno()
    {
        Console.Write("Digite o ano: ");
        if (int.TryParse(Console.ReadLine(), out int ano))
        {
            List<Livro> livros = arvore.ObterLivrosEmOrdem().Where(l => l.Disponivel && l.Ano == ano).ToList();
            if (livros.Count > 0)
            {
                Console.WriteLine("\nLivros encontrados:");
                foreach (Livro l in livros)
                {
                    Console.WriteLine(l);
                    Console.WriteLine("---------------------------------------------");
                }
                Console.Write("Digite o ID do livro desejado: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    return livros.FirstOrDefault(l => l.Id == id);
                }
            }
        }
        return null;
    }

    // Filtrar por Autor
    static Livro FiltrarPorAutor()
    {
        Console.Write("Digite o autor: ");
        string autor = Console.ReadLine().Trim().ToLower();
        List<Livro> livros = arvore.ObterLivrosEmOrdem().Where(l => l.Disponivel && l.Autor.ToLower().Contains(autor)).ToList();
        if (livros.Count > 0)
        {
            Console.WriteLine("\nLivros encontrados:");
            foreach (Livro l in livros)
            {
                Console.WriteLine(l);
                Console.WriteLine("---------------------------------------------");
            }
            Console.Write("Digite o ID do livro desejado: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                return livros.FirstOrDefault(l => l.Id == id);
            }
        }
        return null;
    }

    // Menu de ajuda para o livro encontrado
    static void MenuAjuda(Livro livro)
    {
        Console.WriteLine("\nO que você deseja fazer com este livro?");
        Console.WriteLine("1 - Alugar o livro");
        Console.WriteLine("2 - Retirar para uso presencial");
        Console.WriteLine("3 - Voltar ao submenu de busca");
        Console.Write("Escolha uma opção: ");
        string opcao = Console.ReadLine();

        switch (opcao)
        {
            case "1":
                AlugarLivroPorBusca(livro);
                break;
            case "2":
                RetirarParaUsoPresencial(livro);
                break;
            case "3":
                break;
            default:
                MensagemTemporaria("Opção inválida!");
                break;
        }
    }

    // Aluguel via submenu de busca
    static void AlugarLivroPorBusca(Livro livro)
    {
        if (!livro.Disponivel)
        {
            MensagemTemporaria("Livro indisponível para aluguel.");
            return;
        }
        string dadosCliente = ObterDadosCliente();
        if (!AceitarTermoResponsabilidade("Aluguel"))
        {
            MensagemTemporaria("Termo não aceito. Operação cancelada.");
            return;
        }
        Console.Write("Por quantos dias deseja alugar? ");
        if (!int.TryParse(Console.ReadLine(), out int dias) || dias <= 0)
        {
            MensagemTemporaria("Número de dias inválido.");
            return;
        }
        int grupos = dias / 3;
        int restantes = dias % 3;
        decimal valor = grupos * 10m + restantes * 3m;
        totalAluguel += valor;
        livro.Disponivel = false;
        DateTime retorno = DateTime.Now.AddDays(dias);
        logAluguel.Add($"({livro.Titulo}) alugado por [{dadosCliente}] em {DateTime.Now:dd/MM/yyyy HH:mm:ss} - Devolver em {retorno:dd/MM/yyyy HH:mm:ss}");
        Console.WriteLine($"\nO valor do aluguel para {dias} dia(s) é: R$ {valor:F2}");
        MensagemTemporaria("Aluguel efetuado com sucesso!");
    }

    // Retirada para uso presencial
    static void RetirarParaUsoPresencial(Livro livro)
    {
        if (!livro.Disponivel)
        {
            MensagemTemporaria("Livro indisponível para retirada presencial.");
            return;
        }
        string dadosCliente = ObterDadosCliente();
        if (!AceitarTermoResponsabilidade("Retirada Presencial"))
        {
            MensagemTemporaria("Termo não aceito. Operação cancelada.");
            return;
        }
        Console.Write("Por quantos dias deseja retirar o livro para uso presencial? ");
        if (!int.TryParse(Console.ReadLine(), out int dias) || dias <= 0)
        {
            MensagemTemporaria("Número de dias inválido.");
            return;
        }
        livro.Disponivel = false;
        DateTime retorno = DateTime.Now.AddDays(dias);
        logRetirada.Add($"({livro.Titulo}) retirado para uso presencial por [{dadosCliente}] em {DateTime.Now:dd/MM/yyyy HH:mm:ss} - Devolver em {retorno:dd/MM/yyyy HH:mm:ss}");
        Console.WriteLine("\nRetirada para uso presencial efetuada com sucesso!");
        MensagemTemporaria("Processo concluído!");
    }

    // Listar livros com opção de alugar
    static void ListarLivros()
    {
        Console.Clear();
        Console.WriteLine("=== Lista de Livros Cadastrados ===\n");
        arvore.ListarEmOrdem();

        Console.Write("\nDeseja alugar algum livro? (s/n): ");
        if (Console.ReadLine().Trim().ToLower() == "s")
        {
            AlugarLivro();
            return;
        }

        MostrarRodape();
        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
        Console.ReadLine();
    }

    // Alugar livro (opção geral)
    static void AlugarLivro()
    {
        Console.Clear();
        Console.WriteLine("=== Alugar Livro ===\n");
        Console.WriteLine("Livros disponíveis para aluguel:\n");
        arvore.ListarLivrosDisponiveis();
        Console.WriteLine("\n---------------------------------------------");
        Console.Write("Informe o ID do livro que deseja alugar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            MensagemTemporaria("ID inválido.");
            return;
        }
        var livro = arvore.Buscar(id);
        if (livro == null)
        {
            MensagemTemporaria("Livro não encontrado.");
            return;
        }
        if (!livro.Disponivel)
        {
            MensagemTemporaria("Livro indisponível para aluguel.");
            return;
        }
        string dadosCliente = ObterDadosCliente();
        if (!AceitarTermoResponsabilidade("Aluguel"))
        {
            MensagemTemporaria("Termo não aceito. Operação cancelada.");
            return;
        }
        Console.Write("Por quantos dias deseja alugar? ");
        if (!int.TryParse(Console.ReadLine(), out int dias) || dias <= 0)
        {
            MensagemTemporaria("Número de dias inválido.");
            return;
        }
        int grupos = dias / 3;
        int restantes = dias % 3;
        decimal valor = grupos * 10m + restantes * 3m;
        totalAluguel += valor;
        livro.Disponivel = false;
        DateTime retorno = DateTime.Now.AddDays(dias);
        logAluguel.Add($"({livro.Titulo}) alugado por [{dadosCliente}] em {DateTime.Now:dd/MM/yyyy HH:mm:ss} - Devolver em {retorno:dd/MM/yyyy HH:mm:ss}");
        Console.WriteLine($"\nO valor do aluguel para {dias} dia(s) é: R$ {valor:F2}");
        MensagemTemporaria("Aluguel efetuado com sucesso!");
    }

    // Deletar livro
    static void DeletarLivro()
    {
        Console.Clear();
        Console.WriteLine("=== Deletar Livro ===\n");
        Console.WriteLine("Livros disponíveis:");
        arvore.ListarLivrosDisponiveis();
        Console.WriteLine("\n---------------------------------------------");
        Console.Write("Informe o ID do livro que deseja deletar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            MensagemTemporaria("ID inválido.");
            return;
        }
        var livro = arvore.Buscar(id);
        if (livro == null)
        {
            MensagemTemporaria("Livro não encontrado.");
            return;
        }
        Console.Write("Por qual motivo você deseja deletar? ");
        string motivo = Console.ReadLine();
        logDelecao.Add($"{livro.Titulo} deletado. Motivo: {motivo} em {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
        arvore.Deletar(id);
        arvore.ReorganizarIDs();
        ultimoIdAdicionado = arvore.ObterMaiorID();
        MensagemTemporaria($"Livro deletado com sucesso! Motivo: {motivo}");
    }

    // Área administrativa com extrato e logs
    static void AreaAdministrativa()
    {
        Console.Clear();
        Console.WriteLine("=== Área Administrativa (Caixa) ===\n");
        Console.WriteLine($"Total arrecadado com aluguéis: R$ {totalAluguel:F2}\n");

        List<Livro> todos = arvore.ObterLivrosEmOrdem();
        List<Livro> disponiveis = todos.Where(l => l.Disponivel).ToList();
        List<Livro> naoDisponiveis = todos.Where(l => !l.Disponivel).ToList();

        Console.WriteLine($"Total de livros cadastrados: {todos.Count}");
        Console.WriteLine($"Livros disponíveis: {disponiveis.Count}");
        Console.WriteLine($"Livros alugados/retirados: {naoDisponiveis.Count}\n");

        Console.WriteLine("Últimos livros adicionados (por ID decrescente):");
        int count = 0;
        for (int i = todos.Count - 1; i >= 0 && count < 3; i--, count++)
        {
            Console.WriteLine(todos[i]);
            Console.WriteLine("---------------------------------------------");
        }

        Console.WriteLine("\nÚltimos livros alugados:");
        foreach (string log in logAluguel.TakeLast(3))
        {
            Console.WriteLine(log);
        }

        Console.WriteLine("\nÚltimas retiradas presenciais:");
        foreach (string log in logRetirada.TakeLast(3))
        {
            Console.WriteLine(log);
        }

        Console.WriteLine("\nLivros deletados:");
        foreach (string log in logDelecao.TakeLast(3))
        {
            Console.WriteLine(log);
        }

        Console.WriteLine("\nPressione ENTER para voltar ao menu...");
        Console.ReadLine();
    }

    // Método para carregar livros iniciais
    static void CarregarLivrosImportantes()
    {
        if (arvore.ContarLivros() == 0)
        {
            Livro l1 = new Livro
            {
                Id = 1,
                Titulo = "Dom Casmurro",
                Autor = "Machado de Assis",
                Ano = 1899,
                Historia = "Um clássico da literatura brasileira que explora ciúmes e ambiguidades na narrativa.",
                Disponivel = true
            };

            Livro l2 = new Livro
            {
                Id = 2,
                Titulo = "O Pequeno Príncipe",
                Autor = "Antoine de Saint-Exupéry",
                Ano = 1943,
                Historia = "Uma obra poética que fala sobre amizade, amor e as lições da infância.",
                Disponivel = true
            };

            Livro l3 = new Livro
            {
                Id = 3,
                Titulo = "Memórias Póstumas de Brás Cubas",
                Autor = "Machado de Assis",
                Ano = 1881,
                Historia = "Uma narrativa inovadora que apresenta uma visão crítica e irônica da sociedade.",
                Disponivel = true
            };

            arvore.Inserir(l1);
            arvore.Inserir(l2);
            arvore.Inserir(l3);
            ultimoIdAdicionado = 3;
        }
    }
}

// Classe Livro
public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int Ano { get; set; }
    public string Historia { get; set; }  // Resumo do livro
    public bool Disponivel { get; set; } = true; // Disponibilidade

    public override string ToString()
    {
        string disp = Disponivel ? "Sim" : "Não";
        return $"ID: {Id} | Título: {Titulo} | Autor: {Autor} | Ano: {Ano}\nHistória: {Historia}\nDisponível: {disp}";
    }
}

// Classe No para a árvore
public class No
{
    public Livro Livro { get; set; }
    public No Esquerda { get; set; }
    public No Direita { get; set; }

    public No(Livro livro)
    {
        Livro = livro;
        Esquerda = null;
        Direita = null;
    }
}

// Classe ArvoreABB
public class ArvoreABB
{
    private No raiz;

    public void Inserir(Livro livro)
    {
        raiz = InserirRecursivo(raiz, livro);
    }

    private No InserirRecursivo(No no, Livro livro)
    {
        if (no == null)
            return new No(livro);
        if (livro.Id < no.Livro.Id)
            no.Esquerda = InserirRecursivo(no.Esquerda, livro);
        else if (livro.Id > no.Livro.Id)
            no.Direita = InserirRecursivo(no.Direita, livro);
        else
            Console.WriteLine("ID duplicado! Livro não inserido.");
        return no;
    }

    public Livro Buscar(int id)
    {
        return BuscarRecursivo(raiz, id);
    }

    private Livro BuscarRecursivo(No no, int id)
    {
        if (no == null)
            return null;
        if (id == no.Livro.Id)
            return no.Livro;
        else if (id < no.Livro.Id)
            return BuscarRecursivo(no.Esquerda, id);
        else
            return BuscarRecursivo(no.Direita, id);
    }

    public void ListarEmOrdem()
    {
        if (raiz == null)
            Console.WriteLine("Nenhum livro cadastrado.");
        else
            ListarEmOrdemRecursivo(raiz);
    }

    private void ListarEmOrdemRecursivo(No no)
    {
        if (no != null)
        {
            ListarEmOrdemRecursivo(no.Esquerda);
            Console.WriteLine(no.Livro);
            Console.WriteLine("---------------------------------------------");
            ListarEmOrdemRecursivo(no.Direita);
        }
    }

    public List<Livro> ObterLivrosEmOrdem()
    {
        List<Livro> lista = new List<Livro>();
        ObterLivrosRecursivo(raiz, lista);
        return lista;
    }

    private void ObterLivrosRecursivo(No no, List<Livro> lista)
    {
        if (no != null)
        {
            ObterLivrosRecursivo(no.Esquerda, lista);
            lista.Add(no.Livro);
            ObterLivrosRecursivo(no.Direita, lista);
        }
    }

    public int ContarLivros()
    {
        return ContarRecursivo(raiz);
    }

    private int ContarRecursivo(No no)
    {
        if (no == null)
            return 0;
        return 1 + ContarRecursivo(no.Esquerda) + ContarRecursivo(no.Direita);
    }

    public void Deletar(int id)
    {
        raiz = DeletarRecursivo(raiz, id);
    }

    private No DeletarRecursivo(No no, int id)
    {
        if (no == null)
            return null;
        if (id < no.Livro.Id)
            no.Esquerda = DeletarRecursivo(no.Esquerda, id);
        else if (id > no.Livro.Id)
            no.Direita = DeletarRecursivo(no.Direita, id);
        else
        {
            if (no.Esquerda == null)
                return no.Direita;
            else if (no.Direita == null)
                return no.Esquerda;
            No sucessor = MinValueNode(no.Direita);
            no.Livro = sucessor.Livro;
            no.Direita = DeletarRecursivo(no.Direita, sucessor.Livro.Id);
        }
        return no;
    }

    private No MinValueNode(No no)
    {
        No atual = no;
        while (atual.Esquerda != null)
            atual = atual.Esquerda;
        return atual;
    }

    public void ReorganizarIDs()
    {
        List<Livro> livros = ObterLivrosEmOrdem();
        raiz = null;
        int novoId = 1;
        foreach (Livro livro in livros)
        {
            livro.Id = novoId++;
            Inserir(livro);
        }
    }

    public int ObterMaiorID()
    {
        List<Livro> livros = ObterLivrosEmOrdem();
        if (livros.Count == 0)
            return 0;
        return livros[livros.Count - 1].Id;
    }

    // Lista somente os livros disponíveis
    public void ListarLivrosDisponiveis()
    {
        List<Livro> livros = ObterLivrosEmOrdem();
        foreach (Livro livro in livros)
        {
            if (livro.Disponivel)
            {
                Console.WriteLine(livro);
                Console.WriteLine("---------------------------------------------");
            }
        }
    }
}