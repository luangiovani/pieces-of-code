export interface IGenericoResponse {
    success: boolean;
    message: string;
    obj: any;
}

export interface IGenericoListResponse {
    success: boolean;
    message: string;
    total: number;
    results: any[];
}
