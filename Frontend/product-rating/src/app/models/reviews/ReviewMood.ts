export enum ReviewMood {
    Positive = 1,
    Negative = 2
}

export const ReviewMoodDisplay: { [index: number]: string } = {};
ReviewMoodDisplay[ReviewMood.Positive] = "Positive";
ReviewMoodDisplay[ReviewMood.Negative] = "Negative";