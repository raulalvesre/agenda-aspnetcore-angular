import { FormControl } from '@angular/forms';
import { Component, forwardRef, Input, ViewEncapsulation } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
const INPUT_FIELD_VALUE_ACCESSOR: any = {
  provide: NG_VALUE_ACCESSOR,
  useExisting: forwardRef(() => InputFieldComponent),
  multi: true
}

@Component({
  selector: 'app-input-field',
  templateUrl: './input-field.component.html',
  styleUrls: ['./input-field.component.scss'],
  providers: [INPUT_FIELD_VALUE_ACCESSOR],
  encapsulation : ViewEncapsulation.None,
})
export class InputFieldComponent implements ControlValueAccessor {

  @Input() classeCss: any;
  @Input() id: string;
  @Input() label: string;
  @Input() type = 'text';
  @Input() fxFlex: number;
  @Input() control: any;
  @Input() isReadOnly = false;

  private innerValue: any;
  
  get value() {
    return this.innerValue;
  }

  set value(v: any) {
    if (v !== this.innerValue) {
      this.innerValue = v;
      this.onChangeCb(v);
    }
  }

  onChangeCb: (_: any) => void = () => {};
  onTouchedCb: (_: any) => void = () => {};

  writeValue(v: any): void {
    this.value = v;
  }

  registerOnChange(fn: any): void {
    this.onChangeCb = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouchedCb = fn;
  }

  setDisabledState?(isDisabled: boolean): void {
    this.isReadOnly = isDisabled;
  }

}
