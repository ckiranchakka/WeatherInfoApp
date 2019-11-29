module.exports = {
  root: true,
  env: {
    node: true
  },
  'extends': [
    'plugin:vue/essential',
    '@vue/standard'
  ],
  rules: {
    'no-console': process.env.NODE_ENV === 'production' ? 'error' : 'off',
    'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off',   
      'space-before-blocks': 'off',
      'quotes': 'off',
      'no-extra-semi': 'off',
      'no-trailing-spaces':'off',
      'brace-style':'off',
      'spaced-comment':'off',
      'padding-line-between-statements':'off',
      'indent':'off',
      'padded-blocks':'off',
      'vue/no-side-effects-in-computed-properties':'off',
      'space-before-function-paren':'off'
  },
  parserOptions: {
    parser: 'babel-eslint'
  }
}
