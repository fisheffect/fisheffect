export default {
  lang: {
    code: 'en-US',
    name: 'English',
    decimal: '.',
    thousands: ',',
  },

  currency: {
    USD: {
      prefix: '$',
      precision: '2',
    },
    BRL: {
      prefix: 'BRL$',
      precision: '2',
    },
  },

  country: {
    BRA: {
      name: 'Brazil',
      alpha2: 'BR',
      alpha3: 'BRA',
      lang: {
        code: 'pt-BR',
        name: 'Portuguese',
      },
    },

    USA: {
      name: 'United States',
      alpha2: 'US',
      alpha3: 'USA',
      lang: {
        code: 'en-US',
        name: 'English',
      },
    },
  },

  system: {
    question: {
      confirmRemove: 'Are you sure you want to remove this item?',
    },
    info: {
      welcome: 'Welcome',
    },
    success: {
      resetPassword: 'An E-Mail has been sent to your account',
      recoverPassword: 'The password has successfully changed',
      persist: 'Persisted Successfully!',
    },
    error: {
      unauthorized: 'Restricted Access',
      noServer: 'Could not connect to server',
      validation: 'Validation error',
      required: 'The field \'{0}\' is required',
      invalidEmail: 'The field \'{0}\' must be e-mail',
      invalidDate: 'The field \'{0}\' has not valid date',
      passwordLength: 'The password must have between {0} and {1} characters',
      samePassword: 'The fields password must match',
      length: 'The field \'{0}\' must have between {1} and {2} characters',
      maxLength: 'The field \'{0}\' must not exceed {0} characters',
      minLength: 'The field \'{0}\' must have at least {0} characters',
      min: 'The field \'{0}\' must have a minimum value of {1}',
      max: 'The field \'{0}\' must have a maximum value of {1}',
      invalidAlpha: 'The field \'{0}\' must contain only letters',
      invalidAlphanumeric: 'The field \'{0}\' must contain only letters and numbers',
      invalidCreditCard: 'Invalid card credit number',
      format: 'Wrong format for field \'{0}\'',
      phoneFormat: 'Wrong format for phone number',
      zipcodeFormat: 'Wrong format for zip code',
      rgFormat: 'Wrong format for RG document',
      cpfFormat: 'Wrong format for CPF document',
      cnpjFormat: 'Wrong format for CNPJ document',
    },
  },

  app: {
    title: 'Fish Effect',
    logout: 'Logout',
    menu: 'Menu',
    add: 'Add',
    export: 'Export',
    select: 'Select',
    remove: 'Remove',
    cancel: 'Cancel',
    noDataToShow: 'No data to show',
    downloadCsv: 'Download CSV',
    search: 'Search',
    totalLines: '{total} total lines',
    version: 'Version',
    onlyIfWantChangePassword: 'Fill this field only if you want to change the password',
  },

  dateFormat: {
    date: 'MM/DD/YYYY',
    datetime: 'MM/DD/YYYY hh:mm',
    time: 'hh:mm',
    datemask: '##/##/####',
    datetimemask: '##/##/#### ##:##',
  },

  format: {
    cpf: '###.###.###-##',
    cnpj: '##.###.###/####-##',
    rg: '##.###.###-#',
    cep: '#####-###',
    phone: '(##) #####-####',
  },

  boolean: {
    true: 'Yes',
    false: 'No',
  },

  httpResponse: {
    200: 'Ok',
    400: 'Bad Request',
    401: 'Unauthorized',
    402: 'Payment Required',
    403: 'Forbidden',
    404: 'Not Found',
    405: 'Method not Allowed',
    406: 'Not Acceptable',
  },

  navbar: {
    buyFishCoins: 'Buy Fish Coins',
    about: 'About',
    contact: 'Contact',
    theTeam: 'The Team',
    whitepaper: 'Whitepaper',
    tutorials: 'Tutorials',
  },

  view: {
    login: {
      title: 'Sign-in',
      account: 'Account',
      password: 'Password',
      signin: 'Sign in',
      forgotPassword: 'Forgot password',
    },

    resetPassword: {
      title: 'Forgot Password',
      account: 'E-Mail',
      submit: 'Submit',
      signIn: 'Sign-in',
    },

    recoverPassword: {
      title: 'Recover Password',
      newPassword: 'New password',
      confirmPassword: 'Confirm password',
      submit: 'Submit',
    },

    landingPage: {
      main: {
        title: 'Be the hero of the oceans! Make your own blockchain aquarium',
        panelTitle: 'Limited Offer',
        panelText1: '1 GAS = 500 + 10%',
        panelText2: 'This is NOT a Real ICO See below',
        buyFishCoins: 'Buy Fish Coins',
      },
      tutorials: {
        title: 'This is not a real ICO. This is a tutorial for blockchain minds. Go to the tutorials',
      },
      about: {
        title: 'About',
        subtitle1: 'The first ever Blockchain aquarium!',
        subtitle2: 'Dive into the Neo Blockchain and learn everything you need to make your aquarium!',
        feature1: 'Buy and Sell fishes!',
        feature2: 'More than 2 million Fish Design Possibilities!',
        feature3: 'Feed the Fish',
      },
      whitepaper: {
        title: 'WhiteFishPaper',
        linkTitle: 'Download the Whitepaper',
        linkHref: '#',
      },
      buy: {
        title: 'Buy Fish Coins and help us!',
        subtitle1: 'Fish Coins are used to feed the fish!',
        subtitle2: 'Go ahead, have some fun!',
        buyFishCoins: 'Buy Fish Coins',
      },
      team: {
        title: 'The Fish Effect Team',
        fish1: {
          name: 'Lingodo Mares',
          role: 'CEO',
        },
        fish2: {
          name: 'Piraño Blue',
          role: 'CTO',
        },
        fish3: {
          name: 'Small Mouth Thompson',
          role: 'CFO',
        },
        fish4: {
          name: 'Pinky Shark',
          role: 'Graphics',
        },
      },
      contact: {
        title: 'Contact Us!',
        subtitle1: 'Yes! We do have internet in the middle of the ocean.',
        subtitle2: 'Send as a message!',
        form: {
          name: 'Name',
          email: 'E-Mail',
          message: 'Message',
          send: 'Send',
        },
      },
      footer: {
        copyright: 'Simpli © 2018 // www.simpli.com.br // made in Brazil',
      },
    },
  },

  persist: {
    number: 'Number',
    datetime: 'Datetime',
    submit: 'Submit',
  },

  classes: {
    User: {
      title: 'User',
      columns: {
        idUserPk: 'Id User Pk',
        email: 'Email',
        password: 'Password',
      },
    },
    LoginHolder: {
      title: 'Login Holder',
      columns: {
        email: 'Email',
        password: 'Password',
      },
    },
  },
}
