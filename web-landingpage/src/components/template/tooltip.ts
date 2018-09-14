export default (title: string = '', body: string = '', footer: string = '') => `
<h3 class="text-center">
  ${title}
</h3>
<p class="text-center">
  ${body}
</p>
<p>
  <small>
    <i>${footer}</i>
  </small>
</p>
`
