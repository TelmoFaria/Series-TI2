namespace TF_TI2_19269_19262.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TF_TI2_19269_19262.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //*********************************************************************





            //*********************************************************************
            // adiciona Editora
            var editora = new List<Editora> {
               new Editora {ID=1,Nome="Hbo",Logo="Hbo.jpg" },
               new Editora {ID=2,Nome="Netflix",Logo="Netflix.jpg"},
               new Editora {ID=3,Nome="BBC",Logo="BBC.jpg"},
               new Editora {ID=4,Nome="Editora1",Logo="Editora1.jpg"},
               new Editora {ID=5,Nome="Editora2",Logo="Editora2.jpg"}

            };
            editora.ForEach(ee => context.Editora.AddOrUpdate(e => e.Nome, ee));
            context.SaveChanges();



            // adiciona Series
            var series = new List<Series> {
               new Series {ID=1, Nome="Mr. Robot", Genero="Crime, Drama, Thriller",Sinopse="Elliot (Rami Malek) e um jovem programador que trabalha como engenheiro de seguranca virtual " +
               "durante o dia, e como hacker vigilante durante a noite. Elliot se ve numa encruzilhada quando o lider (Christian Slater) de um misterioso grupo de" +
               " hacker o recruta para destruir a firma que ele e pago para proteger. Motivado pelas suas crencas pessoais, ele luta para resistir a chance de destruir" +
               " os CEOs da multinacional que ele acredita estarem controlando - e destruindo " +
               "- o mundo.",Video="cV1oNnwmdi4",Foto="Serie1.jpg",Classificacao=8.5,EditoraFK=4},
               new Series {ID=2, Nome="Sherlock", Genero="Crime, Drama, Misterio",Sinopse="Baseada nos livros de Sir Arthur Conan Doyle, " +
               "Sherlock conta as aventuras do detetive particular Sherlock Holmes " +
               "(Benedict Cumberbatch) e seu fiel escudeiro, Dr. John Watson " +
               "(Martin Freeman), na Inglaterra dos dias de hoje.",Video="uzyKkKB7mT4",Foto="Serie2.jpg",Classificacao=9.1,EditoraFK=3},
               new Series {ID=3, Nome="Stranger Things", Genero="Drama, Fantasia, Terror",Sinopse="Ambientada na ficticia cidade de Hawkins, Indiana, Stranger " +
               "Things decorre no ano de 1983 e conta a historia de um rapaz que desapareceu misteriosamente. Enquanto procuram por respostas, a policia, " +
               "a familia e os amigos do menino acabam mergulhando num extraordinario misterio envolvendo um experimento secreto do governo, forcas sobrenaturais " +
               "e uma menina muito estranha.",Video="-RcPZdihrp4",
                   Foto ="Serie3.jpg",Classificacao=8.9,EditoraFK=2},
               new Series {ID=4, Nome="The 100", Genero="Drama, Misterio, Ficcao Cientifica",Sinopse="Quando uma guerra nuclear destruiu a civilizacao e o planeta Terra, " +
               "os unicos sobreviventes foram 400 pessoas que estavam em 12 estacoes espaciais em orbita." +
               " 97 anos e tres geracoes depois, a populacao ja contava com 4 mil pessoas, mas os " +
               "recursos ja vao escassos. Para garantir o futuro, um grupo de cem jovens e enviado a superficie da Terra para descobrir se ela esta habitavel",
                   Video ="G-PpFU6tuek",Foto="Serie4.jpg",Classificacao=9.1,EditoraFK=5},
               new Series {ID=5, Nome="Game of Thrones", Genero="Acao, Aventura, Drama",Sinopse="A serie passa-se em Westeros, uma terra reminiscente da Europa Medieval," +
               " onde as estacoes duram anos ou ata mesmo decadas. A historia gira em torno de uma " +
               "batalha entre os Sete Reinos, onde duas familias dominantes lutam pelo controlo do Trono de Ferro, " +
               "cuja posse possivelmente assegurara a sobrevivencia durante o inverno que esta por vir.",Video="BpJYNVhGf1s",
                   Foto ="Serie5.jpg",Classificacao=9.5,EditoraFK=1},

            };
            series.ForEach(ss => context.Series.AddOrUpdate(s => s.Nome, ss));
            context.SaveChanges();






            //*********************************************************************

            //            //*********************************************************************
            //            // adiciona Temporadas
            var temporadas = new List<Temporadas> {
                //Mr.Robot
               new Temporadas {ID=1, Numero=1,Nome="Mr.RobotS01",Foto="Mr.RobotS01.jpg",Trailer="xIBiJ_SzJTA", SerieFK=1},
               new Temporadas {ID=2, Numero=2,Nome="Mr.RobotS02",Foto="Mr.RobotS02.jpg",Trailer="Oc-AsN7d1wg", SerieFK=1},
               new Temporadas {ID=3, Numero=3,Nome="Mr.RobotS03",Foto="Mr.RobotS03.jpg",Trailer="ESLDL8H1qG0", SerieFK=1},
                //sherlock
               new Temporadas {ID=4, Numero=1,Nome="SherlockS01",Foto="SherlockS01.jpg",Trailer="xK7S9mrFWL4", SerieFK=2},
               new Temporadas {ID=5, Numero=2,Nome="SherlockS02",Foto="SherlockS02.jpg",Trailer="bF71IxmOdeE", SerieFK=2},
               new Temporadas {ID=6, Numero=3,Nome="SherlockS03",Foto="SherlockS03.jpg",Trailer="9UcR9iKArd0", SerieFK=2},
               new Temporadas {ID=7, Numero=4,Nome="SherlockS04",Foto="SherlockS04.jpg",Trailer="qlcWFoNqZHc", SerieFK=2},
                //Stranger Things
               new Temporadas {ID=8, Numero=1,Nome="StrangerThingsS01",Foto="StrangerThingsS01.jpg",Trailer="Bb4uR9gTVXI", SerieFK=3},
               new Temporadas {ID=9, Numero=2,Nome="StrangerThingsS02",Foto="StrangerThingsS02.jpg",Trailer="R1ZXOOLMJ8s", SerieFK=3},
               //The 100
               new Temporadas {ID=10, Numero=1,Nome="The100S01",Foto="The100S01.jpg",Trailer="ia1Fbg96vL0", SerieFK=4},
               new Temporadas {ID=11, Numero=2,Nome="The100S02",Foto="The100S02.jpg",Trailer="IW7_ZzFZF-o", SerieFK=4},
               new Temporadas {ID=12, Numero=3,Nome="The100S03",Foto="The100S03.jpg",Trailer="uwxwHTu802M", SerieFK=4},
               new Temporadas {ID=13, Numero=4,Nome="The100S04",Foto="The100S04.jpg",Trailer="JQi2MAfpACI", SerieFK=4},
               new Temporadas {ID=14, Numero=5,Nome="The100S05",Foto="The100S05.jpg",Trailer="7LNTrcguDaw", SerieFK=4},
               new Temporadas {ID=15, Numero=6,Nome="The100S06",Foto="The100S06.jpg",Trailer="l4GiE-8-LWo", SerieFK=4},
               //Game of Thrones
               new Temporadas {ID=16, Numero=1,Nome="GameOfThronesS01",Foto="GameOfThronesS01.jpg",Trailer="gcTkNV5Vg1E", SerieFK=5},
               new Temporadas {ID=17, Numero=2,Nome="GameOfThronesS02",Foto="GameOfThronesS02.jpg",Trailer="XuKfFzk1uQs", SerieFK=5},
               new Temporadas {ID=18, Numero=3,Nome="GameOfThronesS03",Foto="GameOfThronesS03.jpg",Trailer="wBtkdje5OfY", SerieFK=5},
               new Temporadas {ID=19, Numero=4,Nome="GameOfThronesS04",Foto="GameOfThronesS04.jpg",Trailer="xZY43QSx3Fk", SerieFK=5},
               new Temporadas {ID=20, Numero=5,Nome="GameOfThronesS05",Foto="GameOfThronesS05.jpg",Trailer="A0pLbTXPHng", SerieFK=5},
               new Temporadas {ID=21, Numero=6,Nome="GameOfThronesS06",Foto="GameOfThronesS06.jpg",Trailer="yu8eRaq1FUM", SerieFK=5},
               new Temporadas {ID=22, Numero=7,Nome="GameOfThronesS07",Foto="GameOfThronesS07.jpg",Trailer="giYeaKsXnsI", SerieFK=5},
               new Temporadas {ID=23, Numero=8,Nome="GameOfThronesS08",Foto="GameOfThronesS08.jpg",Trailer="rlR4PJn8b8I", SerieFK=5},

            };
            temporadas.ForEach(tt => context.Temporadas.AddOrUpdate(t => t.Nome, tt));
            context.SaveChanges();

            //            //*********************************************************************
            //            // adiciona Episodios
            var episodios = new List<Episodios> {
                            //mr.robot
                            new Episodios {ID=1,Nome="hellofriends",Numero=1,Sinopse="Em MR. ROBOT, Elliot, um engenheiro de segurança cibernética de dia e vigilante hacker à" +
                            " noite, é recrutado por um misterioso grupo clandestino para destruir a empresa que ele pagou para proteger. Elliot deve decidir até onde ele irá " +
                            "expor as forças que acredita estarem correndo (e arruinando) o mundo.",Foto="Mr.RobotT01E01.jpg",Trailer="",Classificacao=9.3, TemporadaFK=1},
                            new Episodios {ID=2,Nome="ones-and-zer0es",Numero=2,Sinopse="Elliot recusa uma oferta de trabalho de Tyrell, dizendo que ele está feliz onde está" +
                            " agora, ou seja, trabalhando para Gideon na AllSafe. Gideon agradece-lhe por salvar a empresa e oferece-lhe um aumento. Ao voltar do trabalho," +
                            " Elliot encontra Darlene em seu apartamento. Eles pegam o trem para o depósito da loja onde ele encontra o Sr. Robô, que tenta convencê-lo a" +
                            " continuar com o hacking que eles começaram juntos. No entanto, Elliot não tem certeza se quer continuar fazendo isso e até mesmo considera " +
                            "entregá-los.",Foto="Mr.RobotT01E02.jpg",Trailer="",Classificacao=8.7, TemporadaFK=1},
                            new Episodios {ID=3,Nome="d3bug",Numero=3,Sinopse="Tyrell é recusado a chance de convencer a empresa que ele é o caminho certo para substituir" +
                            " Colby. Para obter algum conforto, ele paga alguém para levar uma surra. Então ele se aproxima da secretária de seu chefe para obter informações" +
                            " sobre o novo CTO. Elliot acorda no hospital com Krista e Shayla observando-o. Ele precisa convencer Krista de que está bem, mas isso não é" +
                            " problema, já que ele entra facilmente no banco de dados do hospital. Elliot acha que a sociedade é feita com ele e planeja começar uma nova" +
                            " vida - uma vida normal. Daí ele traz Shayla em um jantar no Gideon. Uma vez que mais informações estão sendo reveladas sobre Colby por fsociety," +
                            " Elliot descobre que o jogo não acabou. Nesse meio tempo," +
                            " Ollie está sendo chantageado.",Foto="Mr.RobotT01E03.jpg",Trailer="",Classificacao=8.4, TemporadaFK=1},
                            new Episodios {ID=4,Nome="unm4sk-pt1",Numero=1,Sinopse="Elliot continua vendo seu psicólogo, Krista. O novo personagem Susan Jacobs está tendo" +
                            " problemas com a tecnologia. Gideon faz uma visita a Elliot reclamando que ele foi alvo de um crime que Elliot poderia ter cometido. A Fsociety" +
                            " está de volta à ação, pedindo à E Corp por 5,9 milhões de" +
                            " dólares em 24 horas.",Foto="Mr.RobotT02E01.jpg",Trailer="",Classificacao=8.4, TemporadaFK=2},
                            new Episodios {ID=5,Nome="unm4sk-pt2",Numero=2,Sinopse="Cinco / nove mudou o mundo; Elliot está em reclusão; Angela encontra a felicidade na " +
                            "Evil Corp .; A fsociety entrega uma carga maliciosa.",Foto="Mr.RobotT02E02.jpg",Trailer="",Classificacao=8.4, TemporadaFK=2},
                           //Sherlock
                            new Episodios {ID=6,Nome="Unaired Pilot",Numero=1,Sinopse="Invalidado casa da guerra no Afeganistão, o Dr. John Watson torna-se companheiro de" +
                            " quarto com o único mundo consulting detective, Sherlock Holmes. Within a day their friendship is forged and " +
                            "several murders are solved.",Foto="SherlockT01E01.jpg",Trailer="",Classificacao=8.9, TemporadaFK=4},
                            new Episodios {ID=7,Nome="A Study in Pink",Numero=2,Sinopse="Ferido no Afeganistão enquanto estava no Exército, o Dr. John Watson " +
                           " retorna à Londres contemporânea e, através de um conhecimento mútuo, torna-se acamado no apartamento 221B Baker Street da Sra. Hudson com o" +
                           " brilhante e excêntrico investigador particular Sherlock Holmes. Houve três suicídios aparentes idênticos, e o inspetor Lestrade pede a intervenção de" +
                           " Sherlock no quarto, a morte suspeita de Jennifer Wilson. Enquanto estava deitada, ela escreveu Rache no chão e Sherlock deduz que esse não era " +
                           "apenas o nome incompleto de sua filha natimorta Rachel de muitos anos antes, mas a senha de seu celular. Isso leva Sherlock a confrontar um " +
                           "serial killer terminalmente doente que mata pessoas, em parte, para mostrar superioridade sobre o resto da sociedade, e que está determinado" +
                           " a fazer de Sherlock sua próxima vítima.",Foto="SherlockT01E02.jpg",Trailer="",Classificacao=9.1, TemporadaFK=4},
                            new Episodios {ID=8,Nome="The Blind Banker",Numero=3,Sinopse="O banqueiro Eddie Van Coon e o repórter Brian Lukis são mortos a tiros em " +
                            "assassinatos idênticos, em quartos trancados por dentro. A funcionária do museu chinês Soo Lin conta a Sherlock Holmes que, como órfã " +
                            "adolescente na China, ela administrava drogas para o sindicato do crime da Black Lotus, para quem os dois homens mortos também trabalhavam. " +
                            "Ela também é assassinada, sendo o assassino uma mosca humana capaz de escalar edifícios. A turma confunde Watson por Holmes e captura ele e " +
                            "sua nova namorada, exigindo que Holmes venha em socorro.",Foto="SherlockT01E03.jpg",Trailer="",Classificacao=8.1, TemporadaFK=4},
                            new Episodios {ID=9,Nome="A Scandal in Belgravia",Numero=1,Sinopse="Após um bizarro impasse com o mestre criminoso Moriarty, que terminou quando " +
                            "o vilão respondeu a um telefonema, Sherlock interrompe a investigação de um assassinato rural quando foi chamado ao Palácio de Buckingham." +
                            " Dominatrix Irene Adler tem fotos incriminatórias de uma princesa real que Sherlock está empenhada em recuperar; No entanto, tendo organizado " +
                            "uma reunião com Irene, Sherlock percebe que ela tem muito mais provas perigosas em sua posse, procurada por agentes desonestos da CIA, o que " +
                            "faz com que ela falsifique sua morte e passe os fatos, criptografados em seu celular, para Sherlock. Tendo decifrado o resultado explosivo e " +
                            "descoberto uma conspiração do governo, Sherlock tem que considerar se Irene é confiável e se ela está em aliança " +
                            "com Moriarty.",Foto="SherlockT02E01.jpg",Trailer="",Classificacao=9.5, TemporadaFK=5},
                            new Episodios {ID=10,Nome="The Hounds of Baskerville ",Numero=2,Sinopse="Vinte anos antes, aos sete anos de idade, o jovem Henry Knight viu " +
                            "seu pai ser despedaçado por uma criatura monstruosa em Dewer's Hollow, perto de sua casa em Dartmoor. Agora Henry viu as pegadas de uma enorme" +
                            " fera e suspeita que a estação de pesquisa do governo de Baskerville nas proximidades está criando animais mutantes. Sherlock e John viajam " +
                            "para a charneca onde Fletcher, um rapaz local, organiza passeios turísticos aproveitando a lenda do cão espectral de Dartmoor. Usando falso" +
                            " ID o casal se infiltra em Baskerville e é desafiado pelo secreto Major Barrymore, mas resgatado pelo simpático Dr. Frankland, um amigo de " +
                            "Henry. Depois que o próprio Sherlock vê a criatura monstruosa, ele pede a ajuda do geneticista Dr. Stapleton para ajudá-lo a resolver " +
                            "o mistério.",Foto="SherlockT02E02.jpg",Trailer="",Classificacao=8.5, TemporadaFK=5},
                            //Stranger Things
                            new Episodios {ID=11,Nome="The Vanishing of Will Byers",Numero=1,Sinopse="Algo sai de um laboratório subterrâneo na pacata cidade de Hawkins " +
                            "e sequestra uma criança local. A cidade inteira e os amigos da criança embarcam em uma aventura procurando por ele. Uma garota perseguida" +
                            " por alguns agentes obscuros se cruzará com as crianças locais.",Foto="StrangerThingsT01E01.jpg",Trailer="",Classificacao=8.6, TemporadaFK=8},
                            new Episodios {ID=12,Nome="The Weirdo on Maple Street",Numero=2,Sinopse="Mike esconde a garota misteriosa em sua casa. Joyce recebe um " +
                            "telefonema estranho.",Foto="StrangerThingsT01E02.jpg",Trailer="",Classificacao=8.5, TemporadaFK=8},
                            new Episodios {ID=13,Nome="Holly, Jolly",Numero=3,Sinopse="Uma Nancy cada vez mais preocupada procura por Barb e descobre o que Jonathan " +
                            "tem feito. Joyce está convencida de que Will está tentando" +
                            " falar com ela.",Foto="StrangerThingsT01E03.jpg",Trailer="",Classificacao=8.9, TemporadaFK=8},
                            new Episodios {ID=14,Nome="MADMAX",Numero=1,Sinopse="Perto do Halloween, Mike, Dustin, Lucas e Will acham que a pontuação de Dustin em um" +
                            " jogo de Arcade foi derrotada por alguém com o apelido de MadMax. Eles acreditam que o recém-chegado na escola Maxine é o jogador." +
                            " Hopper investiga o que aconteceu com abóboras que foram envenenadas e o dono culpa seu vizinho. Will tem visões de Upside Down " +
                            "e ele vai com Joyce e Hopper para uma clínica para exame médico.",Foto="StrangerThingsT02E01.jpg",Trailer="",Classificacao=8.4, TemporadaFK=9},
                            new Episodios {ID=15,Nome="Trick or Treat, Freak",Numero=2,Sinopse="Onze lembra como ela escapou de cabeça para baixo e conheceu " +
                            "Hopper. Ela pergunta se ela pode se vestir como um fantasma para fazer doces ou travessuras, mas Hopper diz a ela que seria muito " +
                            "perigoso. Ele promete trazer doces para ela e assistir a um filme de terror com o Eleven. Hopper vai investigar o mistério das " +
                            "abóboras podres. Nancy quer dizer aos pais de Barb que ela está morta, mas Steve avisa que seria um problema para ela com o governo. " +
                            "Dustin e Lucas convidam Max para ir tratar-ou-tratar com eles, Mike e Will, que estão vestidos como os Ghostbusters. Nancy fica" +
                            " bêbada em uma festa de Halloween e diz a Steve que ela não o ama, e Jonathan a leva para casa. Will tem outra visão de um monstro " +
                            "no Upside Down. Onze visitas Mike através do Vazio, mas não consegue entrar em contato com ele. Dustin encontra" +
                            " algo estranho em sua lixeira.",Foto="StrangerThingsT02E02.jpg",Trailer="",Classificacao=8.5, TemporadaFK=9},
                            //The 100
                            new Episodios {ID=16,Nome="Pilot",Numero=1,Sinopse="Os prisioneiros são enviados para a Terra 97 anos depois que os humanos deixaram o" +
                            " planeta depois de uma queda nuclear. Eles são enviados de uma estação espacial onde os humanos agora vivem, mas eles não têm recursos" +
                            " e suprimentos para viver e sobreviver na estação espacial por muito mais tempo. Os 100 são, portanto, usados ​​para determinar se a Terra" +
                            " é mais uma vez habitável, os 100 precisam aprender a se adaptar rapidamente ao seu novo ambiente, mas as facções começam a surgir entre " +
                            "os 100 e nem todos estão jogando bola.",Foto="The100T01E01.jpg",Trailer="",Classificacao=7.6, TemporadaFK=10},
                            new Episodios {ID=17,Nome="Earth Skills",Numero=2,Sinopse="Descobrindo que Jasper ainda pode estar vivo, Clarke, Bellamy, Finn, Wells e" +
                            " Murphy saem para encontrá-lo. Na Arca, Abby está determinado a provar que a Terra é habitável e pede a um mecânico que crie uma cápsula " +
                            "de escape.",Foto="The100T01E02.jpg",Trailer="",Classificacao=7.7, TemporadaFK=10},
                            new Episodios {ID=18,Nome="Earth Kills",Numero=3,Sinopse="Quando uma neblina perigosa e ácida se aproxima, Clarke, Finn e Wells partem em" +
                            " busca de uma planta parecida com uma alga marinha para fazer um emplastro antibiótico para as feridas de Jasper, enquanto uma jovem " +
                            "vulnerável segue quando Bellamy leva um grupo para caçar comida.",Foto="The100T01E03.jpg",Trailer="",Classificacao=8.0, TemporadaFK=10},
                            new Episodios {ID=19,Nome="The 48",Numero=1,Sinopse="Pegando imediatamente onde o final da temporada anterior terminou, Clarke e os 48 " +
                            "exilados juvenis sobreviventes (dos 100 originais) estão sozinhos em seus quartos brancos, tentando freneticamente entender o ambiente" +
                            " bizarro, enquanto os destinos de Bellamy, Finn e Raven ainda são desconhecidos. Fora do laboratório, o plano de Lincoln e Octavia de" +
                            " encontrar seus amigos é frustrado quando um inimigo letal retorna. Enquanto isso, Abby, Kane e as poucas centenas de sobreviventes da" +
                            " Arca devem enfrentar dilemas físicos e morais em seu perigoso e belo mundo novo. Sozinho nos restos orbitais da Arca, Jaha toma uma" +
                            " decisão heróica.",Foto="The100T02E01.jpg",Trailer="",Classificacao=8.6, TemporadaFK=11},
                            new Episodios {ID=20,Nome="Inclement Weather",Numero=2,Sinopse="Clarke confronta o presidente da comunidade Underground, Dante Wallace," +
                            " e exige respostas. Enquanto isso, Kane interroga um dos 100 por informações sobre a área e os Grounders, e Abby realiza uma cirurgia de " +
                            "emergência. Enquanto isso, Octavia recorre à violência para encontrar Lincoln" +
                            " e quem está segurando-o.",Foto="The100T02E02.jpg",Trailer="",Classificacao=8.5, TemporadaFK=11},
                            //Game of Thrones
                            new Episodios {ID=21,Nome="Winter Is Coming",Numero=1,Sinopse="Na terra de Winterfell, Lord Ned Stark começa a acreditar que algo está errado." +
                            " Um desertor da Patrulha da Noite, os guardiões da gigantesca parede de gelo no limite norte de seu território, diz que viu os caminhantes " +
                            "brancos. Mais tarde, Ned e seus filhos encontram animais mortos na floresta, incluindo uma loba cujas seis crias mantêm, uma para cada um" +
                            " dos filhos de Ned. Eles também acolhem a chegada do bom amigo de Ned, o rei Robert, que já foi noivo da irmã de Ned. Ela foi morta antes" +
                            " que eles pudessem se casar e ele se casou com Cersei Lannister, que lhe deu um filho, Joffrey. Junto com eles estão os dois irmãos de Cersei," +
                            " o bonitão Jaime e Tyrion, um anão de muito grande apetite. O rei quer que Ned volte com ele para sua capital, King's Landing, e se torne a Mão" +
                            " do Rei. Ned aceita, mas seu filho, Bran, acidentalmente vê algo que ele não deveria ter e sofre uma queda séria como resultado." +
                            " Enquanto isso, na terra do outro lado do mar estreito, Viserys Targaryen precisa de" +
                            " um exército para atacar o rei ...",Foto="GameOfThronesT01E01.jpg",Trailer="",Classificacao=9.1, TemporadaFK=16},
                            new Episodios {ID=22,Nome="The Kingsroad",Numero=2,Sinopse="Embora seu filho Bran esteja deitado na cama inconsciente de sua queda," +
                            " Ned Stark deve retornar com o Rei Robert a Porto Real. Sua esposa Catelyn fica para trás em Winterfell, embora pareça haver pouca " +
                            "esperança de recuperação de Bran. O rei concordou que seu filho Joffrey e a filha de Ned, Sansa, deveriam se casar, unindo suas famílias" +
                            " para sempre. Os problemas surgem quando Joffrey desafia o filho do açougueiro que está jogando com a irmã mais nova de Sansa, Arya. Joffrey " +
                            "é ferido quando o lobo de estimação de Arya o ataca. A justiça do rei é rápida, mas justa, mesmo que sua esposa Cersei não concorde. Enquanto" +
                            " isso, o filho ilegítimo de Ned segue para o norte para se juntar à Patrulha Noturna. Do outro lado do Mar Estreito, Daenerys está tendo alguma" +
                            " dificuldade em se acomodar na vida de casado. Ela se vira para um de seus servos, um escravo cujo trabalho era uma vez para agradar aos homens," +
                            " para aprender como ela poderia fazer seu marido feliz.",Foto="GameOfThronesT01E02.jpg",Trailer="",Classificacao=8.8, TemporadaFK=16},
                            new Episodios {ID=23,Nome="Lord Snow",Numero=3,Sinopse="Após a tentativa de assassinato do jovem Bran, Catelyn parte para Porto Real " +
                            "para ver seu marido, convencido de que os Lannister estavam por trás da tentativa. Na chegada, ela é recebida por um ex-namorado, Petyr" +
                            " Baelish, que diz a ela que a faca usada no ataque a Bran era dele - até que ele a perdeu em uma aposta para Tyrion Lannister um ano" +
                            " atrás. Quando apresentado com as provas, Ned não está convencido. Ele se mantém ocupado como a Mão do Rei e fica chocado com as" +
                            " formas gastas do rei Robert. Cersei diz a seu filho Joffrey que quando ele é rei, ele pode fazer o que quiser. Do outro lado do Mar Estreito," +
                            " Daenerys começa a agir como uma rainha e tem um confronto com seu irmão, que deixa claro que ele está no comando. Ela também percebe que está " +
                            "grávida. Na parede, o filho ilegítimo de Ned, Jon Snow," +
                            " é treinado para se tornar membro da Patrulha da Noite.",Foto="GameOfThronesT01E03.jpg",Trailer="",Classificacao=8.7, TemporadaFK=16},
                            new Episodios {ID=24,Nome="The North Remembers",Numero=1,Sinopse="O inverno está chegando enquanto a Patrulha da Noite vai para enfrentar" +
                            " a ameaça ao norte da Muralha, o impasse entre os Stark e os Lannisters ferve com novos rumores sobre o direito de primogenitura do Rei " +
                            "Joffrey e a ascensão dos irmãos Baratheon na guerra. Todo o tempo, Dany leva seu bando de coisas quebradas pelas terras mortas que mal se" +
                            " aguentam. Confie ... Joffrey fará o que for preciso" +
                            " para manter sua coroa.",Foto="GameOfThronesT02E01.jpg",Trailer="",Classificacao=8.9, TemporadaFK=17},
                            new Episodios {ID=25,Nome="The Night Lands",Numero=2,Sinopse="Na esteira de uma purga sangrenta na capital, Tyrion castiga Cersei por" +
                            " alienar os súditos do rei. Na estrada para o norte, Arya compartilha um segredo com Gendry, um recruta da Patrulha da Noite." +
                            " Com suprimentos diminuindo, um dos batedores de Dany retorna com a notícia de sua posição. Depois de nove anos como ala de Stark," +
                            " Theon Greyjoy se reúne com seu pai Balon, que quer restaurar o antigo Reino das Ilhas de Ferro. Davos pede a Salladhor Saan," +
                            " um pirata, para unir forças com Stannis e Melisandre para uma " +
                            "invasão naval de King's Landing.",Foto="GameOfThronesT02E02.jpg",Trailer="",Classificacao=8.6, TemporadaFK=17},







            };
            episodios.ForEach(ee => context.Episodios.AddOrUpdate(e => e.Nome, ee));
            context.SaveChanges();

            var pessoas = new List<Pessoas> {
                //Game of Thrones
               new Pessoas {ID=1,Nome="Peter Dinklage",Foto="Ator1.jpg"},
               new Pessoas {ID=2,Nome="Lena Headey",Foto="Ator2.jpg"},
               new Pessoas {ID=3,Nome="Emilia Clarke",Foto="Ator3.jpg"},
               new Pessoas {ID=4,Nome="David Benioff",Foto="Realizador7.jpg"},
               //The 100
               new Pessoas {ID=5,Nome="Eliza Taylor",Foto="Ator4.jpg"},
               new Pessoas {ID=6,Nome="Paige Turco",Foto="Ator5.jpg"},
               new Pessoas {ID=7,Nome="Bob Morley",Foto="Ator6.jpg"},
               new Pessoas {ID=20,Nome="Jason Rothenberg",Foto="Realizador6.jpg"},
               //Stranger Things
               new Pessoas {ID=8,Nome="Winona Ryder",Foto="Ator7.jpg"},
               new Pessoas {ID=9,Nome="David Harbour",Foto="Ator8.jpg"},
               new Pessoas {ID=10,Nome="Finn Wolfhard",Foto="Ator9.jpg"},
               new Pessoas {ID=19,Nome="Matt Duffer",Foto="Realizador4.jpg"},
               //Mr.Robot
               new Pessoas {ID=11,Nome="Rami Malek",Foto="Ator10.jpg"},
               new Pessoas {ID=12,Nome="Christian Slater",Foto="Ator11.jpg"},
               new Pessoas {ID=13,Nome="Carly Chaikin",Foto="Ator12.jpg"},
               new Pessoas {ID=17,Nome="Sam Esmail",Foto="Realizador1.jpg"},
               //Sherlock
               new Pessoas {ID=14,Nome="Benedict Cumberbatch",Foto="Ator13.jpg"},
               new Pessoas {ID=15,Nome="Martin Freeman",Foto="Ator14.jpg"},
               new Pessoas {ID=16,Nome="Una Stubbs",Foto="Ator15.jpg"},
               new Pessoas {ID=18,Nome="Mark Gatiss",Foto="Realizador2.jpg"},

            };
            pessoas.ForEach(pp => context.Pessoas.AddOrUpdate(p => p.Nome, pp));
            context.SaveChanges();

           var utilizadores = new List<Utilizadores>
            {
               new Utilizadores {ID=1, Nome="João Alves", UserName="joao@teste.pt", Email="joao@teste.pt"},
               new Utilizadores {ID=2, Nome="Telmo Faria", UserName="telmo@teste.pt", Email="telmo@teste.pt"},
               new Utilizadores {ID=3, Nome="John Doe", UserName="admin@teste.pt", Email="admin@teste.pt"},

            };
            utilizadores.ForEach(uu => context.Utilizadores.AddOrUpdate(u => u.Nome, uu));
            context.SaveChanges();

            //*********************************************************************
            //            // adiciona Comentarios
            var comentarios = new List<Comentarios> {
               new Comentarios {ID=1,Texto="Não gostei muito deste episódio",EpisodioFK=1, UtilizadorFK=1},
               new Comentarios {ID=2,Texto="Gostei muito",EpisodioFK=2, UtilizadorFK=1},
            };
            comentarios.ForEach(cc => context.Comentarios.AddOrUpdate(c => c.Texto, cc));
            context.SaveChanges();

            var pessoasepisodios = new List<PessoasEpisodios>
            {
               new PessoasEpisodios {ID=1,Papel=(0),EpisodioFK=21,PessoaFK=1},
               new PessoasEpisodios {ID=2,Papel=(0),EpisodioFK=22,PessoaFK=1},
               new PessoasEpisodios {ID=3,Papel=(0),EpisodioFK=23,PessoaFK=1},
               new PessoasEpisodios {ID=4,Papel=(0),EpisodioFK=6,PessoaFK=14},
               new PessoasEpisodios {ID=5,Papel=(0),EpisodioFK=6,PessoaFK=15},
               new PessoasEpisodios {ID=6,Papel=(0),EpisodioFK=6,PessoaFK=16},


            };
            pessoasepisodios.ForEach(rr => context.PessoasEpisodios.AddOrUpdate(r => r.ID, rr));
            context.SaveChanges();


        }
    }
}
