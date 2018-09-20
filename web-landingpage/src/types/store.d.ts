import {ID, Currency, Lang} from '@/simpli'
/**
 * Root
 */
export interface RootState {
  currentSection: string | null
  version: string
  language: Lang
  currency: Currency
  year: number
}
