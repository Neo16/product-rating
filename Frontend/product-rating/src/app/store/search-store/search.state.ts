export interface SearchState {  
   categoryId: string | null;
   brandId: string | null;
   price: {
       minimum: number | null,
       maximum: number | null
   };
   orderBy: 'BEST_SCORE' | 'MOST_REVIEW' | 'PRICE';
}

export const initialState: SearchState = {
   categoryId: null,
   brandId: null,
   price: {
       minimum: 0,
       maximum: null
   },
   orderBy: 'BEST_SCORE'
};