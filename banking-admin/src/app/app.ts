import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';
import { FeatureService, Feature } from './services/feature';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  protected readonly title = signal('banking-admin');
  features = signal<Feature[]>([]);

  constructor(private featureService: FeatureService) {}

  ngOnInit(): void {
    this.featureService.getAll().subscribe({
      next: (data) => this.features.set(data),
      error: (err) => console.error('Feature yüklenemedi:', err),
    });
  }

  isFeatureEnabled(name: string): boolean { //verilen isimde bir feature bulup isEnabled durumunu döndürüyor
    const feature = this.features().find((f) => f.name === name);
    return feature ? feature.isEnabled : false;
  }
}