import { Component, OnInit, signal } from '@angular/core';
import { Customer, Account } from '../services/customer';

@Component({
  selector: 'app-customer-list',
  imports: [],
  templateUrl: './customer-list.html',
  styleUrl: './customer-list.css',
})
export class CustomerList implements OnInit {
  accounts = signal<Account[]>([]);

  constructor(private customerService: Customer) {}

  ngOnInit(): void {
    this.customerService.getAll().subscribe({
      next: (data) => this.accounts.set(data),
      error: (err) => console.error('Hata:', err),
    });
  }
}