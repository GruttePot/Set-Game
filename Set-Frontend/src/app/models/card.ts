export enum CardColour { Red = 'red', Green = 'green', Purple = 'purple' }
export enum CardShape { Diamond = 'diamond', Squiggle = 'squiggle', Oval = 'oval' }
export enum CardFilling { Solid = 'solid', Striped = 'striped', Open = 'open' }
export enum CardNumber { One = 'one', Two = 'two', Three = 'three' }

export interface Card {
  id: number;
  colour: CardColour;
  shape: CardShape;
  filling: CardFilling;
  number: CardNumber;
  selected?: boolean;
}
