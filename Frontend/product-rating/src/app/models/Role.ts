export enum Role {
    Customer = 1,
    WebshopOwner = 2  
}

export const RoleDisplay: { [index: number]: string } = {};
RoleDisplay[Role.Customer] = "Customer";
RoleDisplay[Role.WebshopOwner] = "Webshop owner";