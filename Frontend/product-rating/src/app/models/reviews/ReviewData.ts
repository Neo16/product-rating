import { ReviewMood } from './ReviewMood';

export interface ReviewData
{
    id: string;
    text: string;
    isMine: boolean;
    wasUpvotedByMe: boolean;
    wasDownvotedByMe: boolean;
    mood: ReviewMood;
    points: number;
}