import { ReviewMood } from './ReviewMood';

export class CreateReviewData
{
    mood: ReviewMood;
    text: string;
    productId: string | null;
}