import { Record } from './record';

export interface ApiRecordPage<T extends Record> {
  skip: number;
  take: number;
  total: number;
  data: T[];
}
