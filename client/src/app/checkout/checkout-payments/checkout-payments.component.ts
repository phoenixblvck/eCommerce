import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { BasketService } from '../../basket/basket.service';
import { CheckoutService } from '../checkout.service';
import { ToastrService } from 'ngx-toastr';
import { OrderToCreate } from '../../shared/models/order';
import { Basket } from 'src/app/shared/models/Basket';
import { Address } from 'src/app/shared/models/user';
import { Router, NavigationExtras } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-checkout-payments',
  templateUrl: './checkout-payments.component.html',
  styleUrls: ['./checkout-payments.component.scss'],
})
export class CheckoutPaymentsComponent {
  @Input() checkoutForm?: FormGroup;

  constructor(
    private basketService: BasketService,
    private checkoutService: CheckoutService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  submitOrder() {
    console.log('here')
    const basket = this.basketService.getCurrentBasketValue();
    if (!basket) return;
    const OrderToCreate = this.getOrderToCreate(basket);
    if (!OrderToCreate) return;
    this.checkoutService.createOrder(OrderToCreate).subscribe({
      next: order => {
        this.toastr.success('Order created successfully');
        this.basketService.deleteLocalBasket();
        const navigationExtras: NavigationExtras =  {state: order};
        this.router.navigate(['checkout/success'], navigationExtras);
      }
    })
  }

  pay() {
    const yoco = new (window as any).YocoSDK({
      publicKey: environment.yokoToken,
    });
    yoco.showPopup({
      amountInCents: 5000,
      currency: 'ZAR',
      name: 'Tranquil',
      description: 'Checkout component',
      callback: async (result: any) => {
        // This function returns a token that your server can use to capture a payment
        if (result.error) {
          const errorMessage = result.error.message;
          alert('error occured: ' + errorMessage);
        } else {
          this.submitOrder()
        }
        // In a real integration - you would now pass this chargeToken back to your
        // server along with the order/basket that the customer has purchased.
      },
    });
  };

  private getOrderToCreate(basket: Basket) {
    const deliveryMethod = this.checkoutForm?.get('deliveryForm')?.get('deliveryMethod')?.value;
    const shipToAddress = this.checkoutForm?.get('addressForm')?.value as Address;

    if(!deliveryMethod || !shipToAddress) return;

    return {
      basketId: basket.id,
      deliveryMethodId: deliveryMethod,
      shipToAddress: shipToAddress
    }
  }
}
