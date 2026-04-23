export enum CardColour { Red = 'Red', Green = 'Green', Purple = 'Purple' }
export enum CardShape { Diamond = 'Diamond', Squiggle = 'Squiggle', Oval = 'Oval' }
export enum CardFilling { Solid = 'Solid', Striped = 'Striped', Open = 'Open' }
export enum CardNumber { One = 'One', Two = 'Two', Three = 'Three' }

export interface Card {
  id: number;
  colour: CardColour;
  shape: CardShape;
  filling: CardFilling;
  number: CardNumber;
  selected?: boolean;
  hinted?: boolean;
}
