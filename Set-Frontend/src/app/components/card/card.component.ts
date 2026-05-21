import {Component, Input, OnInit} from '@angular/core';
import {Card, CardColour, CardFilling, CardNumber, CardShape} from '../../models/card';

@Component({
  selector: 'card',
  templateUrl: 'card.component.html',
  styleUrls: ['card.component.scss']
})
export class CardComponent {
  @Input() card!: Card;
  @Input() selected: boolean = false;

  shapeHref: string = '';
  strokeValue: string = '';
  fillValue: string = '';
  shapeArray: number[] = [];

  ngOnInit() {
    this.shapeHref = `#card_${this.card.shape}`
    this.strokeValue = this.card.colour;
    this.fillValue = this.calculateFillValue();
    this.shapeArray = Array.from({ length: this.getShapeCount() }, (_, i) => i);
  }

  private calculateFillValue(): string {
    switch (this.card.filling) {
      case CardFilling.Solid:
        return this.card.colour;
      case CardFilling.Striped:
        return `url(#stripes-${this.card.colour})`;
      case CardFilling.Open:
        return 'none';
      default:
        return this.card.colour;
    }
  }

  private getShapeCount(): number {
    const numbers: { [key in CardNumber]: number } = {
      [CardNumber.One]: 1,
      [CardNumber.Two]: 2,
      [CardNumber.Three]: 3,
    };
    return numbers[this.card.number];
  }

}
