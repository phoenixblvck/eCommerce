import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ContactComponent } from './contact.component';
import { Routes } from '@angular/router';

const routes: Routes = [
  {path: '', component: ContactComponent}
]

@NgModule({
  declarations: [],
  imports: [CommonModule],
})
export class ContactRoutingModule {}
