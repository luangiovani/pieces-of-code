export interface IUsuarioModel {
    id: string;
    cs: string;
    nome: string;
    perfil: string;
    email: string;
    menus: IMenuOpcoesModel[];
}

export interface IMenuOpcoesModel {
    menu_id: string;
    menu_superior_id: string;
    menu_opcao: string;
    controller: string;
    acao: string;
    ordem: number;
    visualizar: boolean;
    cadastrar: boolean;
    excluir: boolean;
    subMenus: IMenuOpcoesModel[];
}

export interface IAuthResponse {
    success: boolean;
    message: string;
    obj: IUsuarioModel;
    token: string;
}
