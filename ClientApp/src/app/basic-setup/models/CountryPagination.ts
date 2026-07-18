import { Country } from "./Country";

export interface ICountryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Country[];
    permission: any;
}
export class CountryPagination implements ICountryPagination {
    totalPages:number;
    itemsFrom:number;
    itemsTo:number;
    totalItemsCount:number;
    items: Country[] = [];
    permission: any;

}
