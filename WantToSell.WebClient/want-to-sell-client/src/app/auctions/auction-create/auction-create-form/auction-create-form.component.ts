import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {MatFormField, MatFormFieldModule, MatLabel} from "@angular/material/form-field";
import {
  MatDatepicker,
  MatDatepickerInput,
  MatDatepickerModule,
  MatDatepickerToggle
} from "@angular/material/datepicker";
import {MatInput, MatInputModule} from "@angular/material/input";
import {MatButton, MatButtonModule, MatIconButton} from "@angular/material/button";
import {MatIcon, MatIconModule} from "@angular/material/icon";
import {MatNativeDateModule} from "@angular/material/core";
import {NgxMaterialTimepickerModule} from "ngx-material-timepicker";

@Component({
  selector: 'app-auction-create-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormField,
    MatDatepickerToggle,
    MatDatepicker,
    MatDatepickerInput,
    MatLabel,
    MatInput,
    MatButton,
    MatIcon,
    MatIconButton,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatIconModule,
    MatButtonModule,
    NgxMaterialTimepickerModule
  ],
  templateUrl: './auction-create-form.component.html',
  styleUrl: './auction-create-form.component.scss'
})
export class AuctionCreateFormComponent implements OnInit {
  auctionForm!: FormGroup;
  fileNames: string = '';

  constructor(private formBuilder: FormBuilder) {
  }

  ngOnInit(): void {
    this.auctionForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      dateExpiredUtc: ['', Validators.required],
      timeExpiredUtc: ['', Validators.required],
      condition: ['', Validators.required],
      categoryId: ['', Validators.required],
      subcategoryId: ['', Validators.required],
      price: ['', Validators.required],
      images: [null],
    });
    }

  handleFileInput(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.fileNames = Array.from(input.files).map((file: File) => file.name).join(', ');
    }
  }

  onSubmit(): void {
    if (this.auctionForm.valid) {
      const formData = new FormData();
      if (this.auctionForm.value.images) {
        const files: File[] = Array.from(this.auctionForm.value.images as FileList);

        files.forEach((file: File) => {
          formData.append('files', file);
        });
      }

      const date = this.auctionForm.get('dateExpiredUtc')?.value;
      const time = this.auctionForm.get('timeExpiredUtc')?.value;

      const dateTime = new Date(date);
      const [hours, minutes] = time.split(':');
      dateTime.setHours(hours, minutes);
    }

  }
}
