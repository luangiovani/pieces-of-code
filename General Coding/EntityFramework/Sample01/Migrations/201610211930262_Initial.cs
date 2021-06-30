namespace TAJ.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        id_area = c.Guid(nullable: false),
                        id_area_mae = c.Guid(),
                        nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        ordem = c.Int(nullable: false),
                        controller = c.String(maxLength: 50, unicode: false),
                        action = c.String(maxLength: 50, unicode: false),
                        help = c.String(maxLength: 8000, unicode: false),
                        ativo = c.Boolean(nullable: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_area)
                .ForeignKey("dbo.Area", t => t.id_area_mae)
                .Index(t => t.id_area_mae);
            
            CreateTable(
                "dbo.Perfil_Area",
                c => new
                    {
                        id_perfil = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_area = c.Guid(nullable: false),
                        ind_visualizar = c.Boolean(nullable: false),
                        ind_cadastrar = c.Boolean(nullable: false),
                        ind_excluir = c.Boolean(nullable: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.id_perfil, t.id_area })
                .ForeignKey("dbo.Area", t => t.id_area)
                .ForeignKey("dbo.Perfil", t => t.id_perfil)
                .Index(t => t.id_perfil)
                .Index(t => t.id_area);
            
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        id_perfil = c.String(nullable: false, maxLength: 128, unicode: false),
                        nome = c.String(nullable: false, maxLength: 256, unicode: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.id_perfil)
                .Index(t => t.nome, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Usuario_Perfil",
                c => new
                    {
                        id_usuario = c.String(nullable: false, maxLength: 128, unicode: false),
                        id_perfil = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.id_usuario, t.id_perfil })
                .ForeignKey("dbo.Usuario", t => t.id_usuario)
                .ForeignKey("dbo.Perfil", t => t.id_perfil)
                .Index(t => t.id_usuario)
                .Index(t => t.id_perfil);
            
            CreateTable(
                "dbo.Bairro",
                c => new
                    {
                        id_bairro = c.Int(nullable: false, identity: true),
                        id_cidade = c.Int(nullable: false),
                        bairro = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id_bairro)
                .ForeignKey("dbo.Cidade", t => t.id_cidade)
                .Index(t => t.id_cidade);
            
            CreateTable(
                "dbo.Cidade",
                c => new
                    {
                        id_cidade = c.Int(nullable: false, identity: true),
                        id_estado = c.Int(nullable: false),
                        cidade = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.id_cidade)
                .ForeignKey("dbo.Estado", t => t.id_estado)
                .Index(t => t.id_estado);
            
            CreateTable(
                "dbo.Estado",
                c => new
                    {
                        id_estado = c.Int(nullable: false, identity: true),
                        id_pais = c.Int(nullable: false),
                        estado = c.String(nullable: false, maxLength: 50, unicode: false),
                        sigla = c.String(maxLength: 2, unicode: false),
                    })
                .PrimaryKey(t => t.id_estado)
                .ForeignKey("dbo.Pais", t => t.id_pais)
                .Index(t => t.id_pais);
            
            CreateTable(
                "dbo.Pais",
                c => new
                    {
                        id_pais = c.Int(nullable: false, identity: true),
                        pais = c.String(nullable: false, maxLength: 50, unicode: false),
                        sigla = c.String(maxLength: 3, unicode: false),
                    })
                .PrimaryKey(t => t.id_pais);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        id_cliente = c.Guid(nullable: false),
                        id_bairro = c.Int(),
                        nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        email = c.String(nullable: false, maxLength: 50, unicode: false),
                        cpf = c.String(maxLength: 14, unicode: false),
                        rg = c.String(maxLength: 8, unicode: false),
                        cep = c.String(maxLength: 9, unicode: false),
                        endereco = c.String(maxLength: 200, unicode: false),
                        numero = c.String(maxLength: 8, unicode: false),
                        telefone = c.String(maxLength: 15, unicode: false),
                        celular = c.String(maxLength: 15, unicode: false),
                        ativo = c.Boolean(nullable: false),
                        taj_pass = c.Boolean(nullable: false),
                        taj_pass_validado = c.Boolean(nullable: false),
                        taj_pass_validade = c.DateTime(),
                        motivo_bloqueio = c.String(maxLength: 500, unicode: false),
                        dt_nascimento = c.DateTime(),
                        dt_cadastro = c.DateTime(nullable: false),
                        auth_token = c.String(nullable: false, maxLength: 100, unicode: false),
                        imagem = c.String(maxLength: 100, unicode: false),
                        vip = c.Boolean(nullable: false),
                        tags = c.String(maxLength: 128, unicode: false),
                        deviceId = c.String(maxLength: 128, unicode: false),
                        deviceSystem = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.id_cliente)
                .ForeignKey("dbo.Bairro", t => t.id_bairro)
                .Index(t => t.id_bairro);
            
            CreateTable(
                "dbo.Filial",
                c => new
                    {
                        id_filial = c.Int(nullable: false, identity: true),
                        id_bairro = c.Int(nullable: false),
                        nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        cep = c.String(nullable: false, maxLength: 9, unicode: false),
                        endereco = c.String(nullable: false, maxLength: 200, unicode: false),
                        numero = c.String(nullable: false, maxLength: 8, unicode: false),
                        descricao = c.String(maxLength: 8000, unicode: false),
                        dt_cadastro = c.DateTime(nullable: false),
                        id_facebook = c.String(maxLength: 128, unicode: false),
                        telefonereserva = c.String(maxLength: 500, unicode: false),
                        textoreserva = c.String(maxLength: 8000, unicode: false),
                        emailreserva = c.String(maxLength: 8000, unicode: false),
                        url_galeria = c.String(maxLength:8000, unicode:false, nullable:true)
                    })
                .PrimaryKey(t => t.id_filial)
                .ForeignKey("dbo.Bairro", t => t.id_bairro)
                .Index(t => t.id_bairro);
            
            CreateTable(
                "dbo.Galeria",
                c => new
                    {
                        id_galeria = c.Guid(nullable: false),
                        id_filial = c.Int(nullable: false),
                        titulo = c.String(nullable: false, maxLength: 100, unicode: false),
                        descritivo = c.String(maxLength: 8000, unicode: false),
                        data = c.DateTime(nullable: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_galeria)
                .ForeignKey("dbo.Filial", t => t.id_filial)
                .Index(t => t.id_filial);
            
            CreateTable(
                "dbo.Foto",
                c => new
                    {
                        id_foto = c.Guid(nullable: false),
                        id_galeria = c.Guid(nullable: false),
                        url = c.String(nullable: false, maxLength: 100, unicode: false),
                        aprovado = c.Boolean(nullable: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_foto)
                .ForeignKey("dbo.Galeria", t => t.id_galeria)
                .Index(t => t.id_galeria);
            
            CreateTable(
                "dbo.Novidade",
                c => new
                    {
                        id_novidade = c.Int(nullable: false, identity: true),
                        id_filial = c.Int(nullable: false),
                        titulo = c.String(nullable: false, maxLength: 100, unicode: false),
                        descritivo = c.String(maxLength: 8000, unicode: false),
                        imagem = c.String(maxLength: 500, unicode: false),
                        dt_cadastro = c.DateTime(nullable: false),
                        id_post_facebook = c.String(maxLength: 128, unicode: false),
                        ver_no_mobile = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_novidade)
                .ForeignKey("dbo.Filial", t => t.id_filial)
                .Index(t => t.id_filial);
            
            CreateTable(
                "dbo.Pharmacy",
                c => new
                    {
                        id_pharmacy = c.Int(nullable: false, identity: true),
                        id_filial = c.Int(nullable: false),
                        nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        descritivo = c.String(maxLength: 8000, unicode: false),
                        imagem = c.String(maxLength: 200, unicode: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_pharmacy)
                .ForeignKey("dbo.Filial", t => t.id_filial)
                .Index(t => t.id_filial);
            
            CreateTable(
                "dbo.Programacao",
                c => new
                    {
                        id_programacao = c.Int(nullable: false, identity: true),
                        id_filial = c.Int(nullable: false),
                        dia_semana = c.String(nullable: false, maxLength: 15, unicode: false),
                        nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        descritivo = c.String(maxLength: 8000, unicode: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_programacao)
                .ForeignKey("dbo.Filial", t => t.id_filial)
                .Index(t => t.id_filial);
            
            CreateTable(
                "dbo.Promocao",
                c => new
                    {
                        id_promocao = c.Int(nullable: false, identity: true),
                        id_filial = c.Int(nullable: false),
                        tipo = c.String(nullable: false, maxLength: 1, unicode: false),
                        nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        descritivo = c.String(maxLength: 8000, unicode: false),
                        imagem = c.String(maxLength: 200, unicode: false),
                        dt_cadastro = c.DateTime(nullable: false),
                        dataHoraPush = c.DateTime(),
                        tags = c.String(maxLength: 128, unicode: false),
                        pushEnviado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_promocao)
                .ForeignKey("dbo.Filial", t => t.id_filial)
                .Index(t => t.id_filial);
            
            CreateTable(
                "dbo.Usuario_Filial",
                c => new
                    {
                        id_filial = c.Int(nullable: false),
                        id_usuario = c.String(nullable: false, maxLength: 128, unicode: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.id_filial, t.id_usuario })
                .ForeignKey("dbo.Filial", t => t.id_filial)
                .ForeignKey("dbo.Usuario", t => t.id_usuario)
                .Index(t => t.id_filial)
                .Index(t => t.id_usuario);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        id_usuario = c.String(nullable: false, maxLength: 128, unicode: false),
                        nome = c.String(nullable: false, maxLength: 50, unicode: false),
                        telefone = c.String(maxLength: 15, unicode: false),
                        celular = c.String(maxLength: 15, unicode: false),
                        ativo = c.Boolean(nullable: false),
                        dt_cadastro = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256, unicode: false),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 128, unicode: false),
                        SecurityStamp = c.String(maxLength: 128, unicode: false),
                        PhoneNumber = c.String(maxLength: 128, unicode: false),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.id_usuario)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Usuario_Claim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                        ClaimType = c.String(maxLength: 128, unicode: false),
                        ClaimValue = c.String(maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Usuario_Login",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128, unicode: false),
                        ProviderKey = c.String(nullable: false, maxLength: 128, unicode: false),
                        UserId = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Usuario", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Playlist",
                c => new
                    {
                        id_playlist = c.Guid(nullable: false),
                        url = c.String(nullable: false, maxLength: 150, unicode: false),
                        dt_cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_playlist);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Usuario_Perfil", "id_perfil", "dbo.Perfil");
            DropForeignKey("dbo.Usuario_Filial", "id_usuario", "dbo.Usuario");
            DropForeignKey("dbo.Usuario_Perfil", "id_usuario", "dbo.Usuario");
            DropForeignKey("dbo.Usuario_Login", "UserId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario_Claim", "UserId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario_Filial", "id_filial", "dbo.Filial");
            DropForeignKey("dbo.Promocao", "id_filial", "dbo.Filial");
            DropForeignKey("dbo.Programacao", "id_filial", "dbo.Filial");
            DropForeignKey("dbo.Pharmacy", "id_filial", "dbo.Filial");
            DropForeignKey("dbo.Novidade", "id_filial", "dbo.Filial");
            DropForeignKey("dbo.Foto", "id_galeria", "dbo.Galeria");
            DropForeignKey("dbo.Galeria", "id_filial", "dbo.Filial");
            DropForeignKey("dbo.Filial", "id_bairro", "dbo.Bairro");
            DropForeignKey("dbo.Cliente", "id_bairro", "dbo.Bairro");
            DropForeignKey("dbo.Bairro", "id_cidade", "dbo.Cidade");
            DropForeignKey("dbo.Cidade", "id_estado", "dbo.Estado");
            DropForeignKey("dbo.Estado", "id_pais", "dbo.Pais");
            DropForeignKey("dbo.Perfil_Area", "id_perfil", "dbo.Perfil");
            DropForeignKey("dbo.Perfil_Area", "id_area", "dbo.Area");
            DropForeignKey("dbo.Area", "id_area_mae", "dbo.Area");
            DropIndex("dbo.Usuario_Login", new[] { "UserId" });
            DropIndex("dbo.Usuario_Claim", new[] { "UserId" });
            DropIndex("dbo.Usuario", "UserNameIndex");
            DropIndex("dbo.Usuario_Filial", new[] { "id_usuario" });
            DropIndex("dbo.Usuario_Filial", new[] { "id_filial" });
            DropIndex("dbo.Promocao", new[] { "id_filial" });
            DropIndex("dbo.Programacao", new[] { "id_filial" });
            DropIndex("dbo.Pharmacy", new[] { "id_filial" });
            DropIndex("dbo.Novidade", new[] { "id_filial" });
            DropIndex("dbo.Foto", new[] { "id_galeria" });
            DropIndex("dbo.Galeria", new[] { "id_filial" });
            DropIndex("dbo.Filial", new[] { "id_bairro" });
            DropIndex("dbo.Cliente", new[] { "id_bairro" });
            DropIndex("dbo.Estado", new[] { "id_pais" });
            DropIndex("dbo.Cidade", new[] { "id_estado" });
            DropIndex("dbo.Bairro", new[] { "id_cidade" });
            DropIndex("dbo.Usuario_Perfil", new[] { "id_perfil" });
            DropIndex("dbo.Usuario_Perfil", new[] { "id_usuario" });
            DropIndex("dbo.Perfil", "RoleNameIndex");
            DropIndex("dbo.Perfil_Area", new[] { "id_area" });
            DropIndex("dbo.Perfil_Area", new[] { "id_perfil" });
            DropIndex("dbo.Area", new[] { "id_area_mae" });
            DropTable("dbo.Playlist");
            DropTable("dbo.Usuario_Login");
            DropTable("dbo.Usuario_Claim");
            DropTable("dbo.Usuario");
            DropTable("dbo.Usuario_Filial");
            DropTable("dbo.Promocao");
            DropTable("dbo.Programacao");
            DropTable("dbo.Pharmacy");
            DropTable("dbo.Novidade");
            DropTable("dbo.Foto");
            DropTable("dbo.Galeria");
            DropTable("dbo.Filial");
            DropTable("dbo.Cliente");
            DropTable("dbo.Pais");
            DropTable("dbo.Estado");
            DropTable("dbo.Cidade");
            DropTable("dbo.Bairro");
            DropTable("dbo.Usuario_Perfil");
            DropTable("dbo.Perfil");
            DropTable("dbo.Perfil_Area");
            DropTable("dbo.Area");
        }
    }
}
