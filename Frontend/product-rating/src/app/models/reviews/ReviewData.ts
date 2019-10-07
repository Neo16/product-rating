import { ReviewMood } from './ReviewMood';

export interface ReviewData
{
    id: string;
    authorId: string;
    text: string;
    isMine: boolean;
    wasUpvotedByMe: boolean;
    wasDownvotedByMe: boolean;
    mood: ReviewMood;
    points: number;
    authorName: string;
    createdAt: string;
    highLighted: boolean;
}