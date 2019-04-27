export enum ProductOrder {
    BestScore = 1,
    Newest = 2,
    MostTextReview = 3,
    Price = 4
}

export const ProductOrderDisplay: { [index: number]: string } = {};

ProductOrderDisplay[ProductOrder.BestScore] = "Best Score";
ProductOrderDisplay[ProductOrder.Newest] = "Newest";
ProductOrderDisplay[ProductOrder.MostTextReview] = "Most Reviews";
ProductOrderDisplay[ProductOrder.Price] = "Price";