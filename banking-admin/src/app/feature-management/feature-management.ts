import { Component, OnInit, signal } from '@angular/core';
import { FeatureService, Feature } from '../services/feature';

@Component({
  selector: 'app-feature-management',
  imports: [],
  templateUrl: './feature-management.html',
  styleUrl: './feature-management.css',
})
export class FeatureManagement implements OnInit {
  features = signal<Feature[]>([]);

  constructor(private featureService: FeatureService) {}

  ngOnInit(): void {
    this.loadFeatures();
  }

  loadFeatures(): void {
    this.featureService.getAll().subscribe({
      next: (data) => this.features.set(data),
      error: (err) => console.error('Feature listesi yüklenemedi:', err),
    });
  }

  toggle(feature: Feature): void {
    const newValue = !feature.isEnabled;
    this.featureService.update(feature.id, newValue).subscribe({
      next: () => this.loadFeatures(),
      error: (err) => console.error('Güncelleme başarısız:', err),
    });
  }
}