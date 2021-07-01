export interface Statement{
    id: number,
    date: string,
    amount : number,
    categoryId: number,
    description: string,
    agentId: string
}

export interface StatementCategory{
    id: number,
    categoryName: string,
    isIncome: boolean,
    agentId: string
}
