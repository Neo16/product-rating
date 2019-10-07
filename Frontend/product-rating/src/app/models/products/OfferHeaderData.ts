import { PictureData } from '../PictureData';

export interface OfferHeaderData
{
    shopId: string;
    shopName: string;
    price: number;
    url: string;
    webShopPicture: PictureData;
}