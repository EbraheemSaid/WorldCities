import { City } from '../cities/city';

export interface Country {
  /** The unique id and primary key for this Country */
  id: number;

  /** Country name (in UTF8 format) */
  name: string;

  /** Country code (in ISO 3166-1 ALPHA-2 format) */
  iso2: string;

  /** Country code (in ISO 3166-1 ALPHA-3 format) */
  iso3: string;

}
