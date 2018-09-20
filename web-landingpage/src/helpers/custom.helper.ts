/** Use this space to create your own custom helper
 * Note1: your custom helper can be accessed in '@/simpli'
 * Note2: be sure the name you have chosen does not exist in simpli-ts-vue
 */
import TooltipTmp from '@/components/template/tooltip'
import {TweenLite} from 'gsap'

require('gsap/ScrollToPlugin')

export const scrollTo = (query: string) =>
  TweenLite.to(window, 1, {scrollTo: {y: query, offsetY: 90, autoKill: false}})

export const tooltip = ({title, body, footer}: any) => ({content: TooltipTmp(title, body, footer)})

export const random = () => Math.random()
